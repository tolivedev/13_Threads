using System;
using System.Threading;

namespace Task01
{
    enum CharsforStrip : int
    {
        A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z
    }
    class Strips
    {
        public Strips()
        {
            offset = new Random().Next(30, Console.WindowWidth - 1);
            rnd = new Random();

        }
        public Strips(int stripPosition)
        {
            offset = stripPosition;
            rnd = new Random();
        }

        //string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        // смещение для столбца всегда будет одинаково в одном потоке
        private readonly int offset;
        static readonly object locker = new object();
        Random rnd;




        //поскольку строку хранить ненадо, выбрасываем каждый раз новый символ
        //из за данного метода, поток может покидать критическую секцию
        private CharsforStrip GetEnumChar()
        {
            return (CharsforStrip)(rnd.Next(26));
            // Приводит строку к массиву символов и по индексу выбрасывает значение из массива, индекс рандомный.
            //  return chars.ToCharArray()[rnd.Next(0, 35)];
        }
        public void ShowChars()
        {
            //для потока создаётся отдельный стек, переменные в потоке будут отдельными копиями.

            int _chainLength; // rnd.Next(4, 10);
            // проверка длины цепочки
            int currentLength;


            // бесконечный повтор полосы
            while (true)
            {
                currentLength = 0;
                _chainLength = 5;
                var startIndErase = 0;

                //задерживаем появление новой следующей цепочки в этом ряду.
                Thread.Sleep(rnd.Next(20, 5000));
                //string NameThread = Thread.CurrentThread.Name;

                // цикл на увеличение местоположения появления цепи
                for (int i = 0; i <= Console.WindowHeight; Thread.Sleep(0))
                {
                    lock (locker)
                    {
                        //Console.WriteLine("{0}", i);
                        Console.CursorTop = i - currentLength;// startIndErase;
                        Console.ForegroundColor = (ConsoleColor)0;

                        if (Console.CursorTop > 0 || currentLength == _chainLength)
                        {
                            //if (_chainLength == 0 && currentLength > Console.WindowHeight - i)
                            if (Console.WindowHeight - i < currentLength)
                            {
                                // Console.CursorTop = i - currentLength - startIndErase;
                                for (int j = 0; j < currentLength - (Console.WindowHeight - i); j++)
                                {
                                    Console.CursorLeft = offset;
                                    Console.WriteLine(" ");
                                }
                                startIndErase++;

                            }

                            Console.CursorLeft = offset;
                            Console.WriteLine(" ");

                        }
                        //увеличиваем текущую длину цепи.    длина цепочки больше текущей показанной - увеличиваем текущую. Показывает сколько длины отображено.
                        if (currentLength < _chainLength) currentLength++;
                        else if (currentLength == _chainLength) _chainLength = 0;

                        // стираем в конце цепочку.   разница высоты окна и номера итерации меньше текущей - уменьшаем текущую показанную
                        if (Console.WindowHeight - i < currentLength) currentLength--;

                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        for (int j = 0; j < currentLength - 2; j++)
                        {
                            Console.CursorLeft = offset;
                            Console.WriteLine(GetEnumChar());

                        }

                        if (currentLength >= 2) //Для каждого второго символа в цепочке
                        {

                            Console.ForegroundColor = ConsoleColor.Green; //устанавливаем цвет
                            Console.CursorLeft = offset; //и позицию в столбце
                            Console.WriteLine(GetEnumChar());

                        }

                        if (currentLength >= 1) //Для каждого первого символа в цепочке
                        {

                            Console.ForegroundColor = ConsoleColor.White; //Устанавливаем цвет
                            Console.CursorLeft = offset; //и позицию в столбце
                            Console.WriteLine(GetEnumChar());
                        }
                        if (i == Console.WindowHeight)
                        {

                            Console.CursorLeft = offset;
                            Console.WriteLine(" ");

                        }
                        Thread.Sleep(25);
                        i++;
                    }

                }
                //Thread.Sleep(50);
            }

        }

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

