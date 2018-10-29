using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace Task02
{
    class TwoStrips
    {
        public TwoStrips()
        {
            offset = new Random().Next(30, Console.WindowWidth - 1);
            rnd = new Random();

        }
        public TwoStrips(int stripPosition)
        {
            offset = stripPosition;
            rnd = new Random();
        }
        public TwoStrips(int stripPosition, bool Second) : this(stripPosition)
        {
            this.IsTwoChain = Second;
            //offset = stripPosition;
            rnd = new Random();
        }

        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        // смещение для столбца всегда будет одинаково в одном потоке
        private readonly int offset;
        static readonly object locker = new object();
        Random rnd;
        bool IsTwoChain = false; // = false;

        //поскольку строку хранить ненадо, выбрасываем каждый раз новый символ
        private char GetEnumChar()
        {
            //return (CharsforStrip)(rnd.Next(26));
            // Приводит строку к массиву символов и по индексу выбрасывает значение из массива, индекс рандомный.
            return chars.ToCharArray()[rnd.Next(0, 35)];
        }
        public void ShowChars()
        {
            //+!+!+!+!+ для потока создаётся отдельный стек, переменные в потоке будут отдельными копиями.
            int bottomBorder = Console.WindowHeight - 2;
            int _chainLength = rnd.Next(4, 10);
            // проверка длины цепочки
            int currentLength;
            Thread SecThread;

            // бесконечный повтор полосы
            while (true)
            {
                currentLength = 0;
                var startIndErase = 0;

                SecThread = new Thread(new TwoStrips(offset, false).ShowChars);
                SecThread.Name = "SecondWork " + SecThread.ManagedThreadId;

                //задерживаем появление новой следующей цепочки в этом ряду.
                Thread.Sleep(rnd.Next(20, 5000));
    

                // цикл на увеличение местоположения появления цепи
                for (int i = 0; i <= bottomBorder; i++)
                {
                    lock (locker)
                    {
                        Console.CursorVisible = false;
                        //Console.WriteLine("{0}", i);
                        Console.CursorTop = i - currentLength;
                        Console.ForegroundColor = (ConsoleColor)0;

                        if (Console.CursorTop > 0 || currentLength == _chainLength)
                        {
                            if (bottomBorder - i < currentLength)
                            {
                                for (int j = 0; j < currentLength - (bottomBorder - i); j++)
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
                        // Second Chain
                        if (IsTwoChain && i < 20 && i > currentLength)
                        {
                            SecThread.Start(); // поток мы можем собирать сразу, а запускать после прохождения условия.
                            // =-=-= из примера видно, что когда первая цепь кончается, может появиться вторая
                            IsTwoChain = false;
                        }
  
                        // стираем в конце цепочку.   разница высоты окна и номера итерации меньше текущей - уменьшаем текущую показанную
                        if (bottomBorder - i < currentLength) currentLength--;

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
                        if (i == bottomBorder)
                        {
                            Console.CursorLeft = offset;
                            Console.WriteLine(" ");
                        }
                        Thread.Sleep(35);
                    }
                    Thread.Sleep(25);
                }
            }
        }
    }
}
