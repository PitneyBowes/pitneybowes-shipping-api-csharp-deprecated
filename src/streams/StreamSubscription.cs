using System;
using Reactive.Streams;
using System.Threading;

namespace streams
{
    public class StreamSubscription : ISubscription
    {
        public bool Cancelled = false;
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        public LongSemaphoreSlim _semaphore;
        StreamPublisher _publisher;
        internal StreamSubscriber Subscriber { get; set; }

        public StreamSubscription(StreamPublisher publisher, StreamSubscriber subscriber)
        {
            Subscriber = subscriber;
            _publisher = publisher;
        }

        public bool IsRequestComplete()
        {
            return _semaphore.Peek() == 0;
        }
        public void Cancel()
        {
            if (Cancelled) return;
            Cancelled = true;
            _publisher.Cancel(this);
        }
        public void Break()
        {
            tokenSource.Cancel();
        }

        public void Wait()
        {
            _semaphore.Wait(tokenSource.Token);
        }

        public void Request(long n)
        {
            if (n < 0) throw new ArgumentException("Request < 0");
            if (!Cancelled)
            {
                _semaphore = new LongSemaphoreSlim(n);
            }
        }
    }
}
