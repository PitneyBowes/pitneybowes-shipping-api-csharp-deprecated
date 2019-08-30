namespace streams
{
    public abstract class StreamComponent
    {
        public bool Running { get; protected set; }
        public abstract void Start();
        public abstract void Stop();
    }
}
