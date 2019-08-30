using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace streams
{


    public abstract class SubscriberHost<T> : StreamComponent
    {
        WorkQueue<T> _workQueue;

        Dictionary<string, StreamSubscriber<T>> _subscribers = new Dictionary<string, StreamSubscriber<T>>();
        protected object _lock = new object();

        public SubscriberHost(int requestLength, bool autoReRequest = true)
        {
            _workQueue = new WorkQueue<T>();
        }

        public void AddSubscriber(string key, StreamSubscriber<T> subscriber)
        {
            lock (_subscribers)
            {
                _subscribers.Add(key, subscriber);
                try
                {
                    subscriber.Pause.Reset(); // Pause the subscriber
                    if (subscriber.WorkQueue != null)
                    {
                        T element;
                        while (subscriber.WorkQueue.TryDequeue(out element)) _workQueue.Enqueue(element);
                        subscriber.WorkQueue = _workQueue;
                    }
                    else
                    {
                        subscriber.WorkQueue = _workQueue; 
                    }
                    _workQueue.MaxLength += subscriber._requestLength;
                }
                finally
                {
                    subscriber.Pause.Set(); // unpause the subscriber
                }
            }
        }

        public StreamSubscriber<T> Subscriber(string key)
        {
            lock (_subscribers)
            {
                return _subscribers[key];
            } 
 
        }

        public override void Stop()
        {
            lock (_lock)
            {
                Running = false;
                List<StreamSubscriber<T>> localList;
                lock (_subscribers)
                {
                    localList = new List<StreamSubscriber<T>> (_subscribers.Values);
                }
                foreach( var s in localList )
                {
                    s.Subscription.Cancel();
                }
            }

        }

        public override void Start()
        {
            lock (_lock)
            {
                if (!Running)
                {
                    Running = true;
                }
            }
            Task.Run((Action)Process);
        }

        public abstract void Action(T element);

        protected virtual void Process()
        {
            while (Running)
            {
                T element;
                _workQueue.WaitObject.WaitOne();
                if (_workQueue.TryDequeue( out element))
                {
                    Console.WriteLine("--{0}", _workQueue.Count);
                    Action(element);
                }
            }
        }
    }
}
