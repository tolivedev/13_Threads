using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Diagnostics;
using System.Threading;

namespace task_test
{

    class ThreadTest
    {
        public bool upper;

        static void Main()
        {
            Thread mainThr = Thread.CurrentThread;
            mainThr.Name = "MainThread";
            ThreadTest instance1 = new ThreadTest();
            instance1.upper = true;
            Thread t = new Thread(instance1.Go);
            t.Start();
            ThreadTest instance2 = new ThreadTest();
            instance2.Go();  // Запуск в главном потоке - с upper=false
        }

        void Go() { Console.WriteLine(upper ? "HELLO!" : "hello!" + " " + Thread.CurrentThread.Name); }
    }

}
