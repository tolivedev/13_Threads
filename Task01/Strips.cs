using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace Task01
{
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
        //объект блокировки должен быть общим для всех, т.е. статическим, поскольку методы вызываются на разных экземплярах класса со своим набором полей.
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
            int bottomBorder = Console.WindowHeight - 2;

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
                for (int i = 0; i <= bottomBorder; Thread.Sleep(0))
                {
                    lock (locker)
                    {
                        Console.CursorVisible = false;
                        //Console.WriteLine("{0}", i);
                        Console.CursorTop = i - currentLength;// startIndErase;
                        Console.ForegroundColor = (ConsoleColor)0;

                        if (Console.CursorTop > 0 || currentLength == _chainLength)
                        {
                            //if (_chainLength == 0 && currentLength > Console.WindowHeight - i)
                            if (bottomBorder - i < currentLength)
                            {
                                // Console.CursorTop = i - currentLength - startIndErase;
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
                        i++;
                    }
                    Thread.Sleep(25);

                }
                //Thread.Sleep(50);
            }

        }

    }
}
