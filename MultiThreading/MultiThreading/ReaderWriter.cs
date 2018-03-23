using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading
{
    public class ReaderWriter
    {
        private static int _counter;
        private static readonly Semaphore queueAcces = new Semaphore(1, 1);
        private static readonly Semaphore readCountAcces = new Semaphore(1, 1);
        private static readonly Semaphore ressoureceAcces = new Semaphore(1, 1);

        public static void Writer()
        {
            while (true)
            {

                queueAcces.WaitOne();
                ressoureceAcces.WaitOne();
                queueAcces.Release();

                Console.WriteLine("Writing");

                ressoureceAcces.Release();

            }
        }

        public static void Reader()
        {
            while (true)
            {
                queueAcces.WaitOne();
                readCountAcces.WaitOne();
                if (_counter == 0) ressoureceAcces.WaitOne();
                _counter++;
                queueAcces.Release();
                readCountAcces.Release();

                Console.WriteLine("Reading");

                readCountAcces.WaitOne();
                _counter--;
                if (_counter == 0) ressoureceAcces.Release();
                readCountAcces.Release();
                Thread.Sleep(500);
            }
        }

        //public static void Main()
        //{
        //    // Queue the task.
        //    ThreadPool.QueueUserWorkItem(ThreadProc);
        //    Console.WriteLine("Main thread does some work, then sleeps.");
        //    Thread.Sleep(1000);

        //    Console.WriteLine("Main thread exits.");
        //}

        // This thread procedure performs the task.
        static void ThreadProc(Object stateInfo)
        {
           
            var i = 0;
            // No state object was passed to QueueUserWorkItem, so stateInfo is null.
            Console.WriteLine("Hello from the thread pool.");
            var list = new List<int> {1, 2, 3, 4, 5};
            //Parallel.For(0,5, index =>
            //{

            //    i = list[i];

            //    Interlocked.Add(ref tmp, i);
            //});

            Parallel.ForEach(list, (element) => { Interlocked.Add(ref i, element); });

            Console.WriteLine();
        }

        public static void Method(int i)
        {

        }


        //public static void Main()
        //{
        //    //var thread1=new Thread(new ThreadStart(Writer)){Name = "Thread 1"};
        //    //var thread2 = new Thread(new ThreadStart(Writer)) { Name = "Thread 2" };
        //    //var thread3 = new Thread(new ThreadStart(Reader)) { Name = "Thread 3" };
        //    //var thread4 = new Thread(new ThreadStart(Reader)) { Name = "Thread 4" };
        //    //var thread5 = new Thread(new ThreadStart(Reader)) { Name = "Thread 5" };

        //    //thread1.Start();
        //    //thread2.Start();
        //    //thread3.Start();
        //    //thread4.Start();
        //    //thread5.Start();


        //    //var employee = new Emplyee() { Name = "Employee1", Salary = 1200 };

        //    //var person = (Person)employee;
        //    //Console.WriteLine(person.Name);
        //}
    }
}

