using System;
using Reactive.Streams;

namespace streams
{
    public abstract class StreamProcessor<T1, T2> : SubscriberHost<T1>, IProcessor<T1, T2>
    {
        private StreamPublisher<T2> _publisher;
        private StreamSubscriber<T1> _subscriber;

        StreamProcessor(int queueSize ) : base( queueSize )
        {
            _publisher = new StreamPublisher<T2>();
            _subscriber = new StreamSubscriber<T1>(queueSize);
            AddSubscriber("Processor", _subscriber);
        }

        public abstract T2 ProcessorAction(T1 element);


        public override void Action(T1 element)
        {
            var t2 = ProcessorAction(element);
            _publisher.Publish(t2);
        }

        public void OnSubscribe(ISubscription subscription)
        {
            _subscriber.OnSubscribe(subscription);
        }

        public void OnNext(T1 element)
        {
            _subscriber.OnNext(element);
        }

        public void OnError(Exception cause)
        {
            _subscriber.OnError(cause);
        }

        public void OnComplete()
        {
            _subscriber.OnComplete();
        }

        public void Subscribe(ISubscriber<T2> subscriber)
        {
            _publisher.Subscribe(subscriber);
        }
    }
}
