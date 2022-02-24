/*
        1.9. Дамба (9)
В рыбохозяйстве "Рыбнастол" принято решение о разведении карасей и щук.
К сожалению, эти рыбы не могут быть вместе в одном водоеме, так как щуки предпочитают питаться исключительно карасями.
Планируется каждое из k озер разделить дамбой на две не сообщающиеся друг с другом части. 
На карте каждое i-е озеро представлено в определенном масштабе прямоугольным  участком  mi × ni единиц,
разбитым на единичные квадраты. Отдельный квадрат целиком занят водой или сушей. 
Множество водных квадратов является связным. Это означает,
что из любого водного квадрата озера можно попасть в любой другой водный квадрат,
последовательно переходя по водным квадратам через их общую сторону. 
Такие последовательности связанных квадратов будем называть путями.
Дамба имеет вид непрерывной ломаной, проходящей по сторонам квадратов.
Два водных квадрата, общая сторона которых принадлежит ломаной, становятся не сообщающимися напрямую друг с другом. 
Требуется для каждого озера определить минимальную по количеству сторон длину дамбы, разделяющей озеро на две не сообщающиеся между собой связные части. 
Чтобы обеспечить доступ рыбаков к воде, каждая из двух полученных частей озера должна сообщаться с берегом. 
Иными словами, каждая часть должна содержать водный квадрат, который либо сам находится на границе исходного прямоугольного участка,
либо имеет общую сторону с квадратом суши, связанным с границей путем из квадратов суши. 
Ввод. В первой строке содержится число k (1 ≤ k ≤ 10). В следующих строках следуют k блоков данных. 
Каждый блок описывает одно озеро. В первой строке блока содержатся числа mi и ni, разделенные пробелом.
В следующих mi строках находится матрица, представляющая озеро, по ni символов в строке. Символ '.' обозначает квадрат суши, 
а символ '#' – квадрат воды. Гарантируется наличие не менее двух водных квадратов. Общая площадь озер s = m1 × n1 + m2 × n2 + … + mk × nk не должна превосходить 106.
Выходные данные. В единственной строке должны выводиться через пробел k значений, 
определяющих минимальные длины дамб. В результате каждое озеро должно быть разделено на две части так, 
что водные клетки из разных частей не могут иметь общей стороны, не принадлежащей дамбе. 
Тем не менее, касание этих клеток углами допускается. Каждая часть должна быть связана с берегом так, как это определялось выше.

    Visual Studio  2019
    Васильев Руслан ПС-24
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace _1._9
{
    internal class Program
    {
        const string inputFileName = "input.txt";
        const string outputFileName = "output.txt";
        static void Main( string[] args )
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            List<Lake> lakes = ReadLakes();
            List<int> result = new List<int>( lakes.Count );
            foreach ( var lake in lakes )
            {
                result.Add( MinDamLength( lake ) );
            }
            Console.WriteLine( String.Join( " ", result ) );
            File.WriteAllText( outputFileName, String.Join( " ", result ) );
            stopWatch.Stop();
            Console.WriteLine( stopWatch.ElapsedMilliseconds );
        }

        private static int MinDamLength( Lake lake )
        {
            if ( lake.points.Length == 12 )
            {
                return 1;
            }
            for ( int y = 0; y < lake.height; y++ )
            {
                for ( int x = 0; x < lake.width; x++ )
                {
                    if ( lake.points[ y, x ] == Type.water )
                    {
                        if ( x + 1 < lake.width && lake.points[ y, x + 1 ] == Type.water )
                        {
                            if ( ( ( y + 1 ) >= lake.height ) || ( lake.points[ y + 1, x ] == Type.land || lake.points[ y + 1, x + 1 ] == Type.land ) )
                            {
                                if ( ( ( y - 1 ) < 0 ) || ( lake.points[ y - 1, x ] == Type.land || lake.points[ y - 1, x + 1 ] == Type.land ) )
                                {
                                    Lake points = (Lake)lake.Clone();
                                    points.points[ y, x + 1 ] = Type.newLake;
                                    var first = CountLandQueue( points, y, x );
                                    // Print( points.points );

                                    if ( first.Item1 == 1 )
                                    {
                                        var hasFirstLakeLand = ( first.Item2.Count == 0 );
                                        if ( !hasFirstLakeLand )
                                        {
                                            hasFirstLakeLand = HasPathToland( (Lake)lake.Clone(), first.Item2 );
                                        }
                                        if ( hasFirstLakeLand )
                                        {
                                            Lake points2 = (Lake)lake.Clone();
                                            points2.points[ y, x + 1 ] = Type.newLake;
                                            var second = HasPathToLandSacond( points2, y, x + 1 );
                                            var hasSecondLakeLand = ( first.Item2.Count == 0 );
                                            if ( hasSecondLakeLand )
                                            {
                                                return 1;
                                            }
                                            if ( HasPathToland( lake, second ) )
                                            {
                                                return 1;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if ( y + 1 < lake.height && lake.points[ y + 1, x ] == Type.water )
                        {
                            if ( ( ( x + 1 ) >= lake.width ) || ( lake.points[ y, x + 1 ] == Type.land || lake.points[ y + 1, x + 1 ] == Type.land ) )
                            {
                                if ( ( ( x - 1 ) < 0 ) || ( lake.points[ y, x - 1 ] == Type.land || lake.points[ y + 1, x - 1 ] == Type.land ) )
                                {
                                    Lake points = (Lake)lake.Clone();
                                    points.points[ y + 1, x ] = Type.newLake;
                                    var first = CountLandQueue( points, y, x );
                                    // Print( points.points );

                                    if ( first.Item1 == 1 )
                                    {
                                        var hasFirstLakeLand = ( first.Item2.Count == 0 );
                                        if ( !hasFirstLakeLand )
                                        {
                                            hasFirstLakeLand = HasPathToland( (Lake)lake.Clone(), first.Item2 );
                                        }
                                        if ( hasFirstLakeLand )
                                        {
                                            Lake points2 = (Lake)lake.Clone();
                                            points.points[ y, x ] = Type.newLake;
                                            var second = HasPathToLandSacond( points2, y + 1, x );
                                            var hasSecondLakeLand = ( first.Item2.Count == 0 );
                                            if ( hasSecondLakeLand )
                                            {
                                                return 1;
                                            }
                                            if ( HasPathToland( lake, second ) )
                                            {
                                                return 1;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return 2;
        }

        private static Queue<(int, int)> HasPathToLandSacond( Lake lake, int y, int x )
        {
            bool hasPathToLand = false;
            var landQueue = new Queue<(int, int)>();
            var queue = new Queue<(int, int)>();
            queue.Enqueue( (y, x) );
            while ( queue.Count > 0 )
            {
                var pos = queue.Dequeue();
                lake.points[ pos.Item1, pos.Item2 ] = Type.land;
                if ( !queue.Contains( (pos.Item1 + 1, pos.Item2) ) )
                {
                    if ( pos.Item1 + 1 < lake.points.GetLength( 0 ) )
                    {
                        if ( lake.points[ pos.Item1 + 1, pos.Item2 ] == Type.water )
                        {
                            queue.Enqueue( (pos.Item1 + 1, pos.Item2) );
                        }
                        else
                        {
                            landQueue.Enqueue( (pos.Item1 + 1, pos.Item2) );
                        }
                    }
                    else
                    {
                        hasPathToLand = true;
                    }
                }
                if ( !queue.Contains( (pos.Item1 - 1, pos.Item2) ) )
                {
                    if ( pos.Item1 - 1 >= 0 )
                    {
                        if ( lake.points[ pos.Item1 - 1, pos.Item2 ] == Type.water )
                        {
                            queue.Enqueue( (pos.Item1 - 1, pos.Item2) );
                        }
                        else
                        {
                            landQueue.Enqueue( (pos.Item1 - 1, pos.Item2) );
                        }
                    }
                    else
                    {
                        hasPathToLand = true;
                    }
                }

                if ( !queue.Contains( (pos.Item1, pos.Item2 + 1) ) )
                {
                    if ( pos.Item2 + 1 < lake.points.GetLength( 1 ) )
                    {
                        if ( lake.points[ pos.Item1, pos.Item2 + 1 ] == Type.water )
                        {
                            queue.Enqueue( (pos.Item1, pos.Item2 + 1) );
                        }
                        else
                        {
                            landQueue.Enqueue( (pos.Item1, pos.Item2 + 1) );
                        }
                    }
                    else
                    {
                        hasPathToLand = true;
                    }
                }

                if ( !queue.Contains( (pos.Item1, pos.Item2 - 1) ) )
                {
                    if ( pos.Item2 - 1 >= 0 )
                    {
                        if ( lake.points[ pos.Item1, pos.Item2 - 1 ] == Type.water )
                        {
                            queue.Enqueue( (pos.Item1, pos.Item2 - 1) );
                        }
                        else
                        {
                            landQueue.Enqueue( (pos.Item1, pos.Item2 - 1) );
                        }
                    }
                    else
                    {
                        hasPathToLand = true;
                    }
                }
            }
            if ( hasPathToLand )
            {
                landQueue.Clear();
            }

            return landQueue;
        }

        private static bool LikeSizeIsOne( Lake points, int y, int x )
        {
            var a = ( x + 1 < points.points.GetLength( 1 ) && points.points[ y, x + 1 ] == Type.land );
            var b = ( y + 1 < points.points.GetLength( 0 ) && points.points[ y + 1, x ] == Type.land );
            var c = ( y - 1 >= 0 && points.points[ y - 1, x ] == Type.land );
            return a && b && c;
        }

        private static bool LikeSizeIsOneReverse( Lake points, int y, int x )
        {
            var a = ( y + 1 < points.points.GetLength( 0 ) && points.points[ y + 1, x ] == Type.land );
            var b = ( x + 1 < points.points.GetLength( 1 ) && points.points[ y, x + 1 ] == Type.land );
            var c = ( x - 1 >= 0 && points.points[ y, x - 1 ] == Type.land );
            return a && b && c;
        }

        static bool HasPathToland( Lake lake, Queue<(int, int)> queue )
        {
            bool result = false;
            while ( queue.Count > 0 )
            {
                var pos = queue.Dequeue();
                lake.points[ pos.Item1, pos.Item2 ] = Type.oldLand;
                if ( !queue.Contains( (pos.Item1 + 1, pos.Item2) ) )
                {
                    if ( pos.Item1 + 1 < lake.points.GetLength( 0 ) -1 )
                    {
                        if ( lake.points[ pos.Item1 + 1, pos.Item2 ] == Type.land )
                        {
                            queue.Enqueue( (pos.Item1 + 1, pos.Item2) );
                        }
                    }
                    else
                    {
                        Console.WriteLine( lake.points[ pos.Item1, pos.Item2 ] );
                        result = true;
                        break;

                    }
                }
                if ( !queue.Contains( (pos.Item1 - 1, pos.Item2) ) )
                {
                    if ( pos.Item1 - 1 > 0 )
                    {
                        if ( lake.points[ pos.Item1 - 1, pos.Item2 ] == Type.land )
                        {
                            queue.Enqueue( (pos.Item1 - 1, pos.Item2) );
                        }
                    }
                    else
                    {
                        result = true;
                        break;
                    }
                }

                if ( !queue.Contains( (pos.Item1, pos.Item2 + 1) ) )
                {
                    if ( pos.Item2 + 1 < lake.points.GetLength( 1 ) -1 )
                    {
                        if ( lake.points[ pos.Item1, pos.Item2 + 1 ] == Type.land )
                        {
                            queue.Enqueue( (pos.Item1, pos.Item2 + 1) );
                        }
                    }
                    else
                    {
                        result = true;
                        break;
                    }
                }

                if ( !queue.Contains( (pos.Item1, pos.Item2 - 1) ) )
                {
                    if ( pos.Item2 - 1 > 0 )
                    {
                        if ( lake.points[ pos.Item1, pos.Item2 - 1 ] == Type.land )
                        {
                            queue.Enqueue( (pos.Item1, pos.Item2 - 1) );
                        }
                    }
                    else
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        static (int, Queue<(int, int)>) CountLandQueue( Lake lake, int y, int x )
        {
            int countСrossings = 0;
            bool hasPathToLand = false;
            var landQueue = new Queue<(int, int)>();
            var queue = new Queue<(int, int)>();
            queue.Enqueue( (y, x) );
            while ( queue.Count > 0 && countСrossings < 2 )
            {
                var pos = queue.Dequeue();
                lake.points[ pos.Item1, pos.Item2 ] = Type.oldLand;
                if ( !queue.Contains( (pos.Item1 + 1, pos.Item2) ) )
                {
                    if ( pos.Item1 + 1 < lake.points.GetLength( 0 ) )
                    {
                        if ( lake.points[ pos.Item1 + 1, pos.Item2 ] == Type.newLake )
                        {
                            countСrossings++;
                        }
                        if ( lake.points[ pos.Item1 + 1, pos.Item2 ] == Type.water )
                        {
                            queue.Enqueue( (pos.Item1 + 1, pos.Item2) );
                        }
                        else
                        {
                            landQueue.Enqueue( (pos.Item1 + 1, pos.Item2) );
                        }
                    }
                    else
                    {
                        hasPathToLand = true;
                    }
                }
                if ( !queue.Contains( (pos.Item1 - 1, pos.Item2) ) )
                {
                    if ( pos.Item1 - 1 >= 0 )
                    {
                        if ( lake.points[ pos.Item1 - 1, pos.Item2 ] == Type.newLake )
                        {
                            countСrossings++;
                        }
                        if ( lake.points[ pos.Item1 - 1, pos.Item2 ] == Type.water )
                        {
                            queue.Enqueue( (pos.Item1 - 1, pos.Item2) );
                        }
                        else
                        {
                            landQueue.Enqueue( (pos.Item1 - 1, pos.Item2) );
                        }
                    }
                    else
                    {
                        hasPathToLand = true;
                    }
                }

                if ( !queue.Contains( (pos.Item1, pos.Item2 + 1) ) )
                {
                    if ( pos.Item2 + 1 < lake.points.GetLength( 1 ) )
                    {
                        if ( lake.points[ pos.Item1, pos.Item2 + 1 ] == Type.newLake )
                        {
                            countСrossings++;
                        }
                        if ( lake.points[ pos.Item1, pos.Item2 + 1 ] == Type.water )
                        {
                            queue.Enqueue( (pos.Item1, pos.Item2 + 1) );
                        }
                        else
                        {
                            landQueue.Enqueue( (pos.Item1, pos.Item2 + 1) );
                        }
                    }
                    else
                    {
                        hasPathToLand = true;
                    }
                }

                if ( !queue.Contains( (pos.Item1, pos.Item2 - 1) ) )
                {
                    if ( pos.Item2 - 1 >= 0 )
                    {
                        if ( lake.points[ pos.Item1, pos.Item2 - 1 ] == Type.newLake )
                        {
                            countСrossings++;
                        }
                        if ( lake.points[ pos.Item1, pos.Item2 - 1 ] == Type.water )
                        {
                            queue.Enqueue( (pos.Item1, pos.Item2 - 1) );
                        }
                        else
                        {
                            landQueue.Enqueue( (pos.Item1, pos.Item2 - 1) );
                        }
                    }
                    else
                    {
                        hasPathToLand = true;
                    }
                }
            }
            if ( hasPathToLand )
            {
                landQueue.Clear();
            }

            return (countСrossings, landQueue);
        }

        private static List<Lake> ReadLakes()
        {
            Queue<string> input = new Queue<string>();
            string[] inputStr = File.ReadAllLines( inputFileName ).ToArray();
            foreach ( var item in inputStr )
            {
                input.Enqueue( item );
            }
            int lakesCount = int.Parse( input.Dequeue() );
            List<Lake> lakes = new List<Lake>();
            for ( int i = 0; i < lakesCount; i++ )
            {
                var size = input.Dequeue().Split( " " ).Select( s => int.Parse( s ) );
                Lake lake = new Lake( size.First() + 2, size.Last() + 2 );

                for ( int y = 0; y < lake.height; y++ )
                {
                    for ( int x = 0; x < lake.width; x++ )
                    {
                        lake.points[ y, x ] = Type.land;
                    }
                }

                for ( int y = 0; y < size.First(); y++ )
                {
                    var line = input.Dequeue();
                    for ( int x = 0; x < size.Last(); x++ )
                    {
                        lake.points[ y + 1, x + 1 ] = (Type)line[ x ];
                    }
                }
                lakes.Add( lake );
            }

            return lakes;
        }

        /// <summary>
        /// debug helper
        /// </summary>
        /// <param name="land"></param>
        static void Print( Type[,] land )
        {
            for ( int i = 0; i < land.GetLength( 0 ); i++ )
            {
                for ( int j = 0; j < land.GetLength( 1 ); j++ )
                {
                    Console.Write( "{0}", (char)land[ i, j ] );
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    struct Lake : ICloneable
    {
        public Lake( int h, int w )
        {
            width = w;
            height = h;
            points = new Type[ h, w ];
        }
        public int width;
        public int height;
        public Type[,] points;

        public object Clone()
        {
            var lake = new Lake( height, width );
            lake.points = (Type[,])points.Clone();

            return lake;
        }
    }

    enum Type
    {
        water = '#',
        land = '.',
        newLake = 'O',
        oldLand = 'x'
    }
}
