using System;
using System.Threading;

namespace MultiThreading.Philosophe
{
    class Philosophe
    {
        int n;
        int thinkDelay;
        int eatDelay;
        int left, right;
        philofork philofork;
        public Philosophe(int n, int thinkDelay, int eatDelay, philofork philofork)
        {
            this.n = n;
            this.thinkDelay = thinkDelay; this.eatDelay = eatDelay;
            this.philofork = philofork;
            left = n;
            right = n==1? 5:n-1;
           
        }
        public void Run()
        {
            for (; ; )
            {
                try
                {
                    Thread.Sleep(thinkDelay);
                    Console.WriteLine($"Thread => {Thread.CurrentThread.Name} ,Philosopher " + n + " waiting to eat...");
                    philofork.Get(left, right);
                    Console.WriteLine($"Thread => {Thread.CurrentThread.Name} ,Philosopher " + n + " is eating...");
                  
                    Thread.Sleep(eatDelay);
                    philofork.Put(left, right);
                    Console.WriteLine($"Thread => {Thread.CurrentThread.Name} ,Philosopher " + n + " finish eating...");
                }
                catch
                {
                    return;
                }
            }
        }

    }
}