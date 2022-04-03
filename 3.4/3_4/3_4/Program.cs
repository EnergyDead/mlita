/*
    3.4. Суперсчастливые билеты (8)
Известно, что «счастливым» билетом называется билет, в номере которого сумма цифр первой половины номера равна сумме цифр второй половины номера. А «суперсчастливым» называется билет, у которого кроме упомянутого условия каждая цифра отличается от соседней не более чем на 1 (например, 323233). Номер может начинаться с 0. Найти количество «суперсчастливых» номеров среди всех 2N-значных билетов (1 ≤ N ≤ 30).
Ввод. Единственная строка содержит значение N.
Вывод. Единственная строка содержит количество номеров.
Примеры
Ввод 1         Ввод 2
3              4 
Вывод 1        Вывод 2
220            1296

    Visual Studio  2019
    Васильев Руслан ПС-24
*/
using System;
using System.IO;

namespace _3_4
{
    public static class Program
    {
        const string inputFileName = "input.txt";
        const string outputFileName = "output.txt";
        public static void Main(string[] args)
        {
            int n = int.Parse(File.ReadAllText(inputFileName));
            ulong ticketsCoun = CalculateTicketsCoun(n);
            Console.WriteLine(ticketsCoun);
        }

        public static ulong CalculateTicketsCoun(int digitsCount)
        {

            Cache cache = new Cache(digitsCount + 1, 10, digitsCount * 9 + 1);
            if(digitsCount == 0)
            {
                return 0;
            }
            ulong count = 0;

            for(int lastDigit = 0; lastDigit <= 9; lastDigit++)
            {
                var maxDigitsSum = GetMaxDigitsSum(lastDigit, digitsCount);
                for(int digitsSum = GetMinDigitsSum(lastDigit, digitsCount); digitsSum <= maxDigitsSum; digitsSum++)
                {
                    var numbersCount = Calculate(digitsCount, lastDigit, digitsSum, cache);
                    ulong temp0 = 0;
                    ulong temp9 = 0;
                    if (lastDigit != 0)
                    {
                        temp0 = Calculate(digitsCount, lastDigit - 1, digitsSum, cache);
                    }
                    if (lastDigit != 9)
                    {
                        temp9 = Calculate(digitsCount, lastDigit + 1, digitsSum, cache);
                    }
                    var temp = numbersCount + temp0 + temp9;
                    count += numbersCount * temp;

                }
            }

            return count;
        }

        private static ulong Calculate(int digitsCount, int lastDigit, int digitsSum, Cache cache)
        {
            {
                if(digitsSum < 0)
                {
                    return 0;
                }
                if(digitsSum == 0 && digitsSum > 0)
                {
                    return 0;
                }
                if(digitsCount == 1)
                {
                    return (ulong) (lastDigit == digitsSum && InRange(lastDigit, 0, 9) ? 1 : 0);
                }
                if(lastDigit == digitsSum && digitsSum == 0)
                {
                    return 1;
                }

                ulong? item = cache.Get(digitsCount, lastDigit, digitsSum);
                var numbersCount = Calculate(digitsCount - 1, lastDigit, digitsSum - lastDigit, cache);
                ulong temp0 = 0;
                if (lastDigit != 0)
                {
                    temp0 = Calculate(digitsCount - 1, lastDigit - 1, digitsSum - lastDigit, cache);
                }
                ulong temp9 = 0;
                if(lastDigit != 9)
                {
                    temp9 = Calculate(digitsCount - 1, lastDigit + 1, digitsSum - lastDigit, cache);
                }
                item ??= numbersCount + temp0 + temp9;
                cache.Set(digitsCount, lastDigit, digitsSum, item);

                return item.Value;
            }
        }

        private static bool InRange(int digit, int min, int max)
        {
            return min <= digit && digit <= max;
        }

        private static int GetMinDigitsSum(int lastDigit, int digitsCount)
        {
            var progressiveCount = Math.Min(digitsCount, lastDigit);
            return ((2 * (lastDigit - progressiveCount + 1) + (progressiveCount - 1)) * progressiveCount) / 2;
        }

        private static int GetMaxDigitsSum(int lastDigit, int digitsCount)
        {
            var progressiveCount = Math.Min(digitsCount, 10 - lastDigit);

            return ((2 * lastDigit + (progressiveCount - 1)) * progressiveCount) / 2 + (digitsCount - progressiveCount) * 9;
        }
    }
}
