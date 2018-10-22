using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace testExample2
{
    class Program
    {

        static void Main()
        { 

            Thread.CurrentThread.Name = "MainThread";

            ThreadTest tt = new ThreadTest();
            ThreadTest tt2 = new ThreadTest();

            Thread th = new Thread(tt.Go);
            th.Start();
            Thread.Sleep(10);
             
            tt2.Go();
            while (true) { }
        }
    }

    class ThreadTest
    {
        bool done;

        //static void Main()
        //{
        //    ThreadTest tt = new ThreadTest(); // Создаем общий объект
        //    new Thread(tt.Go).Start();
        //    tt.Go();
        //}

        // Go сейчас – экземплярный метод
        public void Go()
        {
            if (!done) { done = true; Console.WriteLine("Done"); Thread.Sleep(50); }
        }
    }
}
