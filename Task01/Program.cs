using System;
using System.Threading;

namespace Task01
{
    enum CharsforStrip : int
    {
        A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z
    }
 
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadStart deleg = null;
            Console.CursorVisible = false;
            Thread.CurrentThread.Name = "MainThread";
            Console.SetWindowSize(70, 45);
            for (int i = 0; i < 40; i++)
            {
                Thread thread = new Thread(new Strips(i * 2).ShowChars);
                thread.Name = "Work " + i;
                thread.Start();
                Thread.Sleep(1000);
            }
            Console.ReadKey();
        }
    }
}

