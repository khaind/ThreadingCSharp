using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingCSharp.Part1
{
    /// <summary>
    /// This class uses exclusive lock for thread safety compared to ThreadTest class.
    /// </summary>
    class ThreadSafe
    {
        static bool done;
        static readonly object locker = new object();
        const int LOOP = 5;

        static void Main()
        {
            new Thread(Done).Start();
            Done();

            Console.WriteLine();
            // NOTE: You can wait for another thread to end by calling its Join method
            Thread t = new Thread(Go);
            t.Start();
            t.Join();
            Console.WriteLine("Thread t ended.");

            // NOTE: While waiting on a Sleep or Join, a thread is blocked and so does not consume CPU resources.
            Console.Read();
        }

        private static void Go()
        {
            for (int i = 0; i < LOOP; i++)
            {
                Console.WriteLine("Y");
            }
        }

        private static void Done()
        {
            // NOTE: This ensures only one thread can enter the critical section of code at a time, and “Done” will be printed just once
            lock (locker)
            {
                if(!done)
                {
                    Console.WriteLine("Done");
                    // NOTE: Thread.Sleep pauses the current thread for a specified period
                    //Thread.Sleep(TimeSpan.FromHours(1));  // sleep for 1 hour
                    Thread.Sleep(500);                     // sleep for 500 milliseconds
                    done = true;
                }
            }
        }
    }
}
