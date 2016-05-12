using System;
using System.Threading;

namespace ThreadingCSharp.Part1
{
    class CreatingAndStartingThread
    {
        static void Main()
        {
            // NOTE: threads are created using the Thread class’s constructor, passing in a ThreadStart delegate which indicates where execution should begin
            //Thread t = new Thread(new ThreadStart(Go));
            Thread t = new Thread(Go);    // No need to explicitly use ThreadStart

            // NOTE: Calling Start on the thread then sets it running.
            //  The thread continues until its method returns, at which point the thread ends
            t.Start();   // Run Go() on the new thread.
            Go();        // Simultaneously run Go() in the main thread.


            NextNote();
            // NOTE: Another shortcut is to use a lambda expression or anonymous method:
            Thread t0 = new Thread(() => Console.WriteLine("Hello!"));
            t0.Start();

            NextNote();
            #region PASSING DATA TO THREADS
            // NOTE: The easiest way to pass arguments to a thread’s target method is to execute a lambda expression that calls the method with the desired arguments
            Thread t1 = new Thread(() => Print("Hello from t1!")); 
            t1.Start();

            NextNote();
            // NOTE: Another technique is to pass an argument into Thread’s Start method
            Thread t2 = new Thread(PrintObj); // this will accept "public delegate void ParameterizedThreadStart (object obj)"
            t2.Start("Hello from t2!");

            NextNote();
            // NOTE: be careful about accidentally modifying captured variables after starting the thread, because these variables are shared
            for (int i = 0; i < 10; i++)
                new Thread(() => Console.Write(i)).Start();

            Console.WriteLine();
            Console.Write("Fixed result: ");
            for (int i = 0; i < 10; i++)
            {
                int temp = i;
                new Thread(() => Console.Write(temp)).Start();
            }
            #endregion

            #region THREAD NAMING
            NextNote();
            // Each thread has a Name property that you can set for the benefit of debugging
            Thread.CurrentThread.Name = "main";
            Thread worker = new Thread(Go);
            worker.Name = "worker";
            worker.Start();
            Go();
            #endregion

            #region FOREGROUND vs BACKGROUND THREADS
            // NOTE:Foreground threads keep the application alive for as long as any one of them is running, whereas background threads do not. 
            // Once all foreground threads finish, the application ends, and any background threads still running abruptly terminate.
            // => A thread’s foreground/background status has no relation to its priority or allocation of execution time.

            #endregion

            #region PRIORITY
            // NOTE: A thread’s Priority property determines how much execution time it gets relative to other active threads in the operating system
            // => Think carefully before elevating a thread’s priority — it can lead to problems such as resource starvation for other threads.
            #endregion

            #region EXCEPTION HANDLINGS
            // NOTE: Any try/catch/finally blocks in scope when a thread is created are of no relevance to the thread when it starts executing

            #endregion

            #region THREAD POOLING

            #endregion

            Console.Read();
        }

        private static void PrintObj(object obj)
        {
            string s = (string)(obj);
            Console.WriteLine(s);
        }

        private static void NextNote()
        {
            Console.WriteLine();
            Thread.Sleep(100);
        }

        private static void Print(string msg)
        {
            Console.WriteLine(msg);
        }

        static void Go()
        {
            Console.WriteLine("hello from:" + Thread.CurrentThread.Name);
        }

    }
}
