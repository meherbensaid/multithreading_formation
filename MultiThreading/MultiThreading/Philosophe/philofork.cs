using System.Threading;

namespace MultiThreading.Philosophe
{
    class philofork
    {
        bool[] fork = new bool[5];
        public void Get(int left, int right)
        {
            lock (this)
            {
                while (fork[left] || fork[right]) Monitor.Wait(this);
                fork[left] = true; fork[right] = true;
            }
        }
        public void Put(int left, int right)
        {
            lock (this)
            {
                fork[left] = false; fork[right] = false;
                Monitor.PulseAll(this);
            }
        }
    }
}