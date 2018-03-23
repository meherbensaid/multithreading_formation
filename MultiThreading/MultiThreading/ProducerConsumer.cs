using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading
{
    public  class ProducerConsumer
    {
        private static Semaphore empty = new Semaphore(3, 3);
        private static Semaphore full = new Semaphore(0, 3);
        private static Mutex mutex = new Mutex();

        public static void Produce()
        {
            while (true)
            {
                Console.WriteLine($"Thread :{Thread.CurrentThread.Name}  Want To Produce");
                empty.WaitOne();
                mutex.WaitOne();
                Console.WriteLine($"Thread :{Thread.CurrentThread.Name} Producing");
                Thread.Sleep(500);
                mutex.ReleaseMutex();
                full.Release();
                Console.WriteLine($"Thread :{Thread.CurrentThread.Name}  Produced");
            }
        }

        public static void Consume()
        {
            while (true)
            {
                Console.WriteLine($"Thread :{Thread.CurrentThread.Name}  Want To Consume");
                full.WaitOne();
                mutex.WaitOne();
                Thread.Sleep(1000);
                
                Console.WriteLine($"Thread :{Thread.CurrentThread.Name}  Consuming ");
                mutex.ReleaseMutex();
                empty.Release();
                Console.WriteLine($"Thread :{Thread.CurrentThread.Name}  Consumed");
                
            }

        }

        //public static void Main()
        //{
        //    Thread newThread1 = new Thread(new ThreadStart(Produce)) {Name = "Producer1"};
        //    Thread newThread2 = new Thread(new ThreadStart(Produce)) {Name = "Producer2"};
        //    Thread newThread3 = new Thread(new ThreadStart(Consume)) {Name = "Consumer1"};
        //    Thread newThread4 = new Thread(new ThreadStart(Consume)) {Name = "Consumer2"};
        //    Thread newThread5 = new Thread(new ThreadStart(Consume)) {Name = "Consumer3"};

        //    newThread1.Start();
        //    newThread2.Start();
        //    newThread3.Start();
        //    newThread4.Start();
        //    newThread5.Start();
        //}
    }
}
