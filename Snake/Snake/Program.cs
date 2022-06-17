/*
    19.6. Змейка (3)  
    Змей Горыныч убедительно просит расположить N2 чисел от 1 до N2 в квадрате змейкой. 
    Ввод. В единственной строке файла INPUT.TXT задано число N (1 ≤  N ≤ 50). 
    Вывод. Файл OUTPUT.TXT содержит N2  чисел по N  чисел в строке в форме змейки.
    Пример 
    Ввод
    4
    Вывод
    1 2 6 7 
    3 5 8 13
    4 9 12 14  
    10 11 15 16

    Visual Studio  2019
    Васильев Руслан ПС-24
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Snake
{
    class Program
    {
        static readonly string inputFileName = "input.txt";
        static readonly string outputFileName = "output.txt";
        static void Main(string[] args)
        {
            string inp = File.ReadAllText(inputFileName);
            int N = int.Parse(inp);

            if (N == 1)
            {
                File.WriteAllText(outputFileName, "1");

                return;
            }

            var max = (int)Math.Pow(N, 2);
            var state = State.addUp;
            var field = new List<List<int>>();
            for (int i = 0; i < N; i++)
            {
                field.Add(Enumerable.Range(0, N).ToList());
            }
            int x = 0;
            int y = 0;
            var numbs = new Queue<int>(Enumerable.Range(2, max - 2).ToList());

            field[0][0] = 1;

            int d = N / 2;
            N--;

            while (numbs.Any())
            {
                if (state == State.addUp)
                {
                    if (x < N)
                    {
                        x++;
                        field[y][x] = numbs.Dequeue();
                        state = State.Lower;
                    }
                    else if (numbs.Peek() > d)
                    {
                        y++;
                        field[y][x] = numbs.Dequeue();
                        state = State.Lower;
                    }
                }

                if (state == State.addLower)
                {
                    if (y < N)
                    {
                        y++;
                        field[y][x] = numbs.Dequeue();
                        state = State.Up;
                    }
                    else if (numbs.Peek() > d)
                    {
                        x++;
                        field[y][x] = numbs.Dequeue();
                        state = State.Up;
                    }
                }

                if (state == State.Up)
                {
                    while (y > 0 && x < N)
                    {
                        y--;
                        x++;
                        field[y][x] = numbs.Dequeue();
                    }
                    state = State.addUp;
                }
                if (state == State.Lower)
                {
                    while (x > 0 && y < N)
                    {
                        y++;
                        x--;
                        field[y][x] = numbs.Dequeue();
                    }
                    state = State.addLower;
                }
            }
            field[N][N] = max;

            File.WriteAllText(outputFileName, ConvertToString(field));
        }

        static string ConvertToString(List<List<int>> field)
        {
            string result = string.Empty;
            foreach (var item in field)
            {
                result += string.Join(" ", item.Select(x => x.ToString()));
                result += "\n";
            }
            return result;
        }
    }

    enum State
    {
        addUp,
        addLower,
        Up,
        Lower
    }
}
