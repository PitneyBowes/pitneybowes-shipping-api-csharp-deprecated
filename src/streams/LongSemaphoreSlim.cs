using System;
using System.Threading;
using System.Threading.Tasks;

namespace streams
{
    public class LongSemaphoreSlim
    {
        long _counter;
        object _lock = new object();
        SemaphoreSlim _semaphore;
        public LongSemaphoreSlim(long initialValue)
        {
            lock (_lock)
            {
                _counter = initialValue;
                if (initialValue == 0)
                {
                    _semaphore = new SemaphoreSlim(0);
                }
                else
                {
                    _semaphore = new SemaphoreSlim(1);
                }
            }
        }
        public long Release(long n = 1)
        {
            lock (_lock)
            {
                if (_counter == 0)
                {
                    _semaphore.Release();
                }
                _counter += n;
                return _counter;
            }
        }
        public long Wait(CancellationToken token)
        {
            lock (_lock)
            {
                try
                {
                    if (_counter == 0)
                    {
                        _semaphore.Wait(token);
                    }
                    else if (_counter == 1)
                    {
                        _semaphore.Wait(token);
                    }
                    return _counter--;
                }
                catch (TaskCanceledException)
                {
                    return -1;
                }
            }
        }
        public long Peek()
        {
            Console.WriteLine("Peek: {0}", _counter);
            return _counter;
        }
    }
}
