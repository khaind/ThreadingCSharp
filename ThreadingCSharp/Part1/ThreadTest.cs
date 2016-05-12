using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingCSharp.Part1
{
    class ThreadTest
    {
        const int LOOP = 5;
        bool done;
        static bool doneStatic;

        static void Main()
        {
            //Thread t = new Thread(WriteY);          // Kick off a new thread
            Thread t = new Thread(new ThreadTest().WriteY2);
            t.Start();                              // Running WriteY

            // Simultaneously, do sth on main thread
            for (int i = 0; i < LOOP; i++)
            {
                Console.Write("X");
            }

            Console.WriteLine();
            // Note: The CLR assigns each thread its own memory stack so that local variables are kept separate
            new Thread(Go).Start();
            Go();                           // 2 thread will print 2 * LOOP times of '?'

            Console.WriteLine();
            // NOTE: Threads share data if they have a common reference to the same object instance.
            ThreadTest tt = new ThreadTest();
            new Thread(tt.Go2).Start();
            tt.Go2();

            Console.WriteLine();
            // NOTE: Static fields offer another way to share data between threads
            new Thread(Go3).Start();
            Go3();

            Console.Read();
        }

        private static void Go3()
        {
            if (!doneStatic) { doneStatic = true; Console.WriteLine("Done"); }
        }

        private static void Go()
        {
            for (int cycle = 0; cycle < LOOP; cycle++)
            {
                Console.WriteLine("?");
            }
        }

        void Go2()
        {
            if (!done) {
                done = true;
                Console.WriteLine("Done");
            }
        }

        private static void WriteY()
        {
            for (int i = 0; i < LOOP; i++)
            {
                Console.Write("Y");
            }
        }

        private void WriteY2()
        {
            for (int i = 0; i < LOOP; i++)
            {
                Console.Write("Y");
            }
        }
    }
}
