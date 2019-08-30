using System.Collections.Concurrent;
using System.Threading;

namespace streams
{
    
    public class WorkQueue<T>
    {
        private ConcurrentQueue<T> queue = new ConcurrentQueue<T>();
        public WaitHandle WaitObject { get => _waitHandle; }
        internal EventWaitHandle _waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);

        public int MaxLength { get; set; }

        public void Enqueue(T item)
        {
            lock (queue)
            {
                _waitHandle.Set();
                queue.Enqueue(item);
            }
        }

        public bool TryDequeue(out T element)
        {
            lock (queue)
            {
                var rtn = queue.TryDequeue(out element);
                if (Count == 0) _waitHandle.Reset();
                return rtn;
            }
        }

        public int Count { get => queue.Count; }
    }
}
