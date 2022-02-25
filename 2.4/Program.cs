/*
    2.4. Космический путь (8)
Экспедиция готовится отправиться в путь на космическом корабле нового поколения. 
Планируется последовательно посетить N планет звездной системы: от планеты Земля до планеты Победа. 
Планеты пронумерованы от 1 до N в порядке их посещения, Земля имеет номер 1, а Победа - номер N.
Для перелёта между планетами корабль может использовать любой тип топлива, существующий в звездной системе. 
Перед началом экспедиции корабль находится на планете Земля, и бак корабля пуст. Существующие типы топлива пронумерованы целыми числами, 
на планете с номером i можно заправиться только топливом типа ai. При посещении i-й планеты можно заправиться, 
полностью освободив бак от имеющегося топлива и заполнив его топливом типа ai.
На каждой планете станция заправки устроена таким образом, что в бак заправляется ровно столько топлива, 
сколько потребуется для перелета до следующей планеты с топливом такого же типа. Если далее такой тип топлива не встречается, 
заправляться на этой планете невозможно. Иначе говоря, после заправки на i-й планете топлива хватит для посещения планет от (i+1)-й до j-й включительно, 
где j -  минимальный номер планеты, такой что j > i и aj  = ai. Для продолжения экспедиции дальше j-й планеты корабль необходимо снова заправить на одной из этих планет.
Требуется написать программу, которая по заданным типам топлива на планетах определяет минимальное количество заправок, требуемых для экспедиции.
Ввод. В первой строке входного файла INPUT.TXT записано число N (2 ≤ N ≤ 300000) - количество планет. 
Во второй строке N целых чисел a1, a2, . . . , aN (2 ≤ ai ≤ 300000) - типы топлива на планетах.
Вывод. В первой строке выходного файла OUTPUT.TXT выведите единственное число K - минимальное количество заправок,
которые нужно произвести. Во второй строке выведите K чисел, разделённых пробелами: номера планет, на которых требуется заправиться.
Номера планет требуется выводить в порядке времени заправок. Если решений с минимальным количеством заправок несколько, выведите любое из них.
Если решения не существует, выведите число 0.

    Visual Studio 2019
    Васильев Руслан ПС-24

*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _2._4
{
    internal class Program
    {
        const string inputFileName = "input.txt";
        const string outputFileName = "output.txt";
        const int MAX = int.MaxValue / 2;

        static void Main( string[] args )
        {
            string[] input = File.ReadAllLines( inputFileName );
            int countNumber = int.Parse( input[ 0 ] );
            List<int> fiel = input[ 1 ].Trim().Split( " " ).Select( ch => int.Parse( ch ) ).ToList();

            List<int> right = Enumerable.Repeat( 0, countNumber + 1 ).ToList();
            List<int> left = Enumerable.Repeat( 0, 1 + 300 * 1000 ).ToList();

            for ( int i = countNumber; i >= 1; i-- )
            {
                right[ i ] = left[ fiel[ i - 1 ] ];
                left[ fiel[ i - 1 ] ] = i;
            }

            var segmentLength = Enumerable.Repeat( MAX, countNumber + 1 ).ToList();
            var next = Enumerable.Repeat( 0, countNumber + 1 ).ToList();
            List<Pair> tree = Enumerable.Repeat( new Pair() { segmentLength = MAX, index = 0 }, 2 + 2 * countNumber ).ToList();


            segmentLength[ countNumber ] = 0;
            Update( tree, countNumber, new Pair { segmentLength = 0, index = countNumber } );
            for ( int i = countNumber - 1; i >= 1; i-- )
            {
                if ( right[ i ] > 0 )
                {
                    Pair ans = GetMin( tree, i + 1, right[ i ] );
                    segmentLength[ i ] = ans.segmentLength + 1;
                    next[ i ] = ans.index;
                }
                Update( tree, i, new Pair { segmentLength = segmentLength[ i ], index = i } );
            }

            var result = "0";
            if ( segmentLength[ 1 ] < MAX )
            {
                var res = new List<int>();
                for ( int i = 1; i < countNumber; i = next[ i ] )
                {
                    res.Add( i );
                }
                result = res.Count.ToString();
                result += '\n';
                result += String.Join( " ", res );
            }
            File.WriteAllText( outputFileName, result );
        }

        static void Update( List<Pair> tree, int index, Pair value )
        {
            index += tree.Count / 2;
            tree[ index ] = value;
            while ( index > 1 )
            {
                index /= 2;
                tree[ index ] = Min( tree[ index * 2 ], tree[ index * 2 + 1 ] );
            }
        }

        static Pair GetMin( List<Pair> tree, int left, int right )
        {
            left += tree.Count / 2;
            right += tree.Count / 2;
            Pair result = new Pair
            {
                segmentLength = MAX,
                index = 0
            };
            while ( left <= right )
            {
                if ( left % 2 == 0 )
                {
                    left /= 2;
                }
                else
                {
                    result = Min( result, tree[ left ] );
                    left++;
                    left /= 2;
                }
                if ( right % 2 == 1 )
                {
                    right /= 2;
                }
                else
                {
                    result = Min( result, tree[ right ] );
                    right--;
                    right /= 2;
                }
            }

            return result;
        }

        static Pair Min( Pair left, Pair right )
        {
            if ( left.segmentLength > right.segmentLength )
                return right;
            return left;
        }

        struct Pair
        {
            public int segmentLength;
            public int index;
        }

    }

}