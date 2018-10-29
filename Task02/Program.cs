using System;
using System.Threading;

namespace Task02
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadStart deleg = null;
            
            Thread.CurrentThread.Name = "MainThread";
            Console.SetWindowSize(70, 45);
            Console.CursorVisible = false;
            for (int i = 0; i < 25; i++)
            {
                Thread thread = new Thread(new TwoStrips(i * 4, true).ShowChars); // в этом примере в конструктор первого потока передаётся параметр true
                thread.Name = "Work " + i;
                thread.Start();
                Thread.Sleep(1000);
            }
            Console.ReadKey();
        }
    }
}
