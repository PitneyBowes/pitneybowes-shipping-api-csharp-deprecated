using System;
using System.Threading.Tasks;

namespace streams
{
    public abstract class PublisherHost<T> : StreamComponent
    {
        protected object _lock = new object();
        public StreamPublisher<T> Publisher { get; set; }
        public int Sleep { get; set; }

        public override void Start()
        {
            Running = true;
            Task.Run((Action)Process);
        }

        public override void Stop()
        {
            Running = false;
            Publisher.UnSubscribeAll();
            Publisher.Pause.Set();
        }
        public abstract T Action();
        public virtual bool Done { get => false; }

        protected virtual void Process()
        {
            while (Running && !Done)
            {
                Publisher.Pause.WaitOne();
                if (!Publisher.HasSubscribers()) continue;
                var element = Action();
                Publisher.Publish(element);
                if (Sleep > 0) Task.WaitAll(Task.Delay(Sleep));
            }
            if (Running) Stop();
        }

    }

}
