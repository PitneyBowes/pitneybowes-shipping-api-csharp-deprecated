using System;
using Reactive.Streams;
using System.Threading;

namespace streams
{
    public abstract class StreamSubscriber
    { }

    public class StreamSubscriber<T> : StreamSubscriber, ISubscriber<T>
    {
        internal WorkQueue<T> WorkQueue { get; set; }
        internal EventWaitHandle Pause = new EventWaitHandle(true, EventResetMode.ManualReset);

        internal StreamSubscription Subscription { get; set; }
        internal int _requestLength { get; set; }
        protected bool _autoReRequest { get; set; }
        protected bool _subscriptionEnded { get; set; }

        public StreamSubscriber(int requestLength, bool autoReRequest = true)
        {
            _requestLength = requestLength;
            _autoReRequest = autoReRequest;
        }

        public virtual void OnComplete()
        {
            _subscriptionEnded = true;
            Console.WriteLine("OnComplete");
        }

        public virtual void OnError(Exception cause)
        {
            _subscriptionEnded = true;
        }

        public virtual void OnNext(T element)
        {
            if (_subscriptionEnded) return;
            Pause.WaitOne();
            if (_autoReRequest && Subscription.IsRequestComplete()) Subscription.Request(Math.Min(_requestLength, WorkQueue.MaxLength - WorkQueue.Count));
            WorkQueue.Enqueue(element);
        }

        public virtual void OnSubscribe(ISubscription subscription)
        {
            var sub = subscription as StreamSubscription;
            if (sub == null)
            {
                subscription.Cancel();
                return;
            }
            Subscription = sub;
            Pause.WaitOne();
            if (WorkQueue == null)
            {
                WorkQueue = new WorkQueue<T>();
                WorkQueue.MaxLength = _requestLength;
            }
            Subscription.Request(_requestLength);
        }
    }
}
