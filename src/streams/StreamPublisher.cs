using System;
using System.Collections.Generic;
using System.Threading;
using Reactive.Streams;

namespace streams
{
    public abstract class StreamPublisher
    {
        internal abstract void Cancel(StreamSubscription subscription);
    }

    public class StreamPublisher<T> : StreamPublisher, IPublisher<T>
    {
        List<StreamSubscriber> _subscribers;
        internal EventWaitHandle Pause = new EventWaitHandle(false, EventResetMode.ManualReset);
        CancellationTokenSource tokenSource = new CancellationTokenSource();

        public StreamPublisher()
        {
                _subscribers = new List<StreamSubscriber>();
        }

        internal override void Cancel(StreamSubscription subscription)
        {
            var sub = subscription as StreamSubscription;
            if (sub == null) return;
            lock (_subscribers)
            {
                _subscribers.Remove(sub.Subscriber);
            }
        }

        public void Subscribe(ISubscriber<T> subscriber)
        {
            if (subscriber == null) throw new ArgumentNullException("subscriber cannor be null");
            if (_subscribers.Contains(subscriber as StreamSubscriber<T>)) return;
            lock (_subscribers)
            {
                _subscribers.Add(subscriber as StreamSubscriber<T>);
            }
            subscriber.OnSubscribe(new StreamSubscription(this, subscriber as StreamSubscriber<T>));
            Pause.Set();

        }

        public void UnSubscribe(ISubscriber<T> subscriber)
        {
            var sub = (StreamSubscriber<T>)subscriber;
            lock (_subscribers)
            {
                if (!_subscribers.Remove(sub)) return;
            }
            sub.OnComplete();
            sub.Subscription.Break();
            if (_subscribers.Count == 0)
            {
                Pause.Reset();
            }
        }

        public void UnSubscribeAll()
        {
            List<StreamSubscriber> localList;
            Pause.Reset();
            lock (_subscribers)
            {
                localList = new List<StreamSubscriber>(_subscribers);
            }
            foreach (var subscriber in localList)
            {
                var sub = subscriber as StreamSubscriber<T>;
                if (sub == null) continue;
                sub.OnComplete();
                sub.Subscription.Break();
            }
            lock (_subscribers)
            {
                _subscribers.Clear();
            }
        }

        public bool HasSubscribers()
        {
            lock (_subscribers)
            {
                return _subscribers.Count != 0;
            }

        }

        public void Publish(T element)
        {
            Pause.WaitOne();
            try
            {
                List<StreamSubscriber> localList;
                lock (_subscribers)
                {
                    localList = new List<StreamSubscriber>(_subscribers);
                }
                foreach (var subscriber in localList)
                {
                    var sub = subscriber as StreamSubscriber<T>;
                    if (sub == null) continue;
                    sub.OnNext(element);
                    sub.Subscription.Wait();
                }
                return;
            }
            catch (Exception e)
            {
                List<StreamSubscriber> localList;
                lock (_subscribers)
                {
                    localList = new List<StreamSubscriber>(_subscribers);
                }
                foreach (var subscriber in localList)
                {
                    var sub = subscriber as StreamSubscriber<T>;
                    if (sub == null) continue;
                    sub.OnError(e);
                }
                lock (_subscribers)
                {
                    _subscribers.Clear();
                }

                Pause.Reset();
                return;
            }

        }
    }

}
