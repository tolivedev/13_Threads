using System;
using System.Threading;

namespace _13_Threads
{
    class TestRecursionClass
    {
        int counter;

        public void RecursionOnThreads()
        {
            Console.WriteLine("{0} name " + counter, Thread.CurrentThread.Name);

            counter++;

            (new Thread(RecursionOnThreads) { Name = "Thread " + counter }).Start();

        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            //ParameterizedThreadStart paramDeleg = new ParameterizedThreadStart((new TestRecursionClass()).RecursionOnThreads);

            Thread thread = new Thread(new TestRecursionClass().RecursionOnThreads) { Name = "Thread" };
            //var counter = 5;
            thread.Start();

            Console.ReadKey();
        }
    }
}
