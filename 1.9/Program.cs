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
        const string inputFileName = "input8.txt";
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
                            if ( ( ( y + 1 ) >= lake.height ) || ( lake.points[ y + 1, x ] == Type.land || lake.points[ y + 1, x + 1 ] == Type.land )
                                                              || ( lake.points[ y + 1, x ] == Type.landGood || lake.points[ y + 1, x + 1 ] == Type.landGood ) )
                            {
                                if ( ( ( y - 1 ) < 0 ) || ( lake.points[ y - 1, x ] == Type.land || lake.points[ y - 1, x + 1 ] == Type.land )
                                                       || ( lake.points[ y - 1, x ] == Type.land || lake.points[ y - 1, x + 1 ] == Type.landGood ) )
                                {
                                    Lake points = (Lake)lake.Clone();
                                    points.points[ y, x + 1 ] = Type.newLake;

                                    if ( CorrectDam( points, y, x ) )
                                    {
                                        bool a = CheackedLand( points.points, (y, x) );
                                        bool a2 = CheackedLand2( points.points, (y, x + 1) );
                                        if ( a && a2 )
                                            return 1;
                                    }
                                }
                            }
                        }
                        if ( y + 1 < lake.height && lake.points[ y + 1, x ] == Type.water )
                        {
                            if ( ( ( x + 1 ) >= lake.width ) || ( lake.points[ y, x + 1 ] == Type.land || lake.points[ y + 1, x + 1 ] == Type.land )
                                                             || ( lake.points[ y, x + 1 ] == Type.landGood || lake.points[ y + 1, x + 1 ] == Type.landGood ) )
                            {
                                if ( ( ( x - 1 ) < 0 ) || ( lake.points[ y, x - 1 ] == Type.land || lake.points[ y + 1, x - 1 ] == Type.land )
                                                       || ( lake.points[ y, x - 1 ] == Type.land || lake.points[ y + 1, x - 1 ] == Type.landGood ) )
                                {
                                    Lake points = (Lake)lake.Clone();
                                    points.points[ y + 1, x ] = Type.newLake;

                                    if ( CorrectDam( points, y, x ) )
                                    {
                                        bool a = CheackedLand( points.points, (y, x) );
                                        bool a2 = CheackedLand2( points.points, (y, x + 1) );
                                        if ( a && a2 )
                                            return 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return 2;
        }

        private static bool CheackedLand2( Type[,] points, (int y, int x) p )
        {
            bool hasLand = false;
            var queue = new Queue<(int, int)>();
            queue.Enqueue( p );
            while ( queue.Count > 0 || hasLand )
            {
                var pos = queue.Dequeue();
                points[ pos.Item1, pos.Item2 ] = Type.newNewLake;
                if ( !queue.Contains( (pos.Item1 + 1, pos.Item2) ) )
                {
                    if ( points[ pos.Item1 + 1, pos.Item2 ] == Type.landGood )
                    {
                        return true;
                    }
                    if ( points[ pos.Item1 + 1, pos.Item2 ] == Type.water )
                    {
                        queue.Enqueue( (pos.Item1 + 1, pos.Item2) );
                    }
                }
                if ( !queue.Contains( (pos.Item1 - 1, pos.Item2) ) )
                {
                    if ( points[ pos.Item1 - 1, pos.Item2 ] == Type.landGood )
                    {
                        return true;
                    }
                    if ( points[ pos.Item1 - 1, pos.Item2 ] == Type.water )
                    {
                        queue.Enqueue( (pos.Item1 - 1, pos.Item2) );
                    }
                }
                if ( !queue.Contains( (pos.Item1, pos.Item2 + 1) ) )
                {

                    if ( points[ pos.Item1, pos.Item2 + 1 ] == Type.landGood )
                    {
                        return true;
                    }
                    if ( points[ pos.Item1, pos.Item2 + 1 ] == Type.water )
                    {
                        queue.Enqueue( (pos.Item1, pos.Item2 + 1) );
                    }
                }
                if ( !queue.Contains( (pos.Item1, pos.Item2 - 1) ) )
                {
                    if ( points[ pos.Item1, pos.Item2 - 1 ] == Type.landGood )
                    {
                        return true;
                    }
                    if ( points[ pos.Item1, pos.Item2 - 1 ] == Type.water )
                    {
                        queue.Enqueue( (pos.Item1, pos.Item2 - 1) );
                    }
                }
            }

            return false;
        }

        private static bool CheackedLand( Type[,] points, (int y, int x) p )
        {
            Type type = points[ p.y, p.x ];
            bool hasLand = false;
            var queue = new Queue<(int, int)>();
            queue.Enqueue( p );
            while ( queue.Count > 0 || hasLand )
            {
                var pos = queue.Dequeue();
                points[ pos.Item1, pos.Item2 ] = Type.oldnewLand;
                if ( !queue.Contains( (pos.Item1 + 1, pos.Item2) ) )
                {
                    if ( points[ pos.Item1 + 1, pos.Item2 ] == Type.landGood )
                    {
                        return true;
                    }
                    if ( points[ pos.Item1 + 1, pos.Item2 ] == type )
                    {
                        queue.Enqueue( (pos.Item1 + 1, pos.Item2) );
                    }
                }
                if ( !queue.Contains( (pos.Item1 - 1, pos.Item2) ) )
                {
                    if ( points[ pos.Item1 - 1, pos.Item2 ] == Type.landGood )
                    {
                        return true;
                    }
                    if ( points[ pos.Item1 - 1, pos.Item2 ] == type )
                    {
                        queue.Enqueue( (pos.Item1 - 1, pos.Item2) );
                    }
                }
                if ( !queue.Contains( (pos.Item1, pos.Item2 + 1) ) )
                {

                    if ( points[ pos.Item1, pos.Item2 + 1 ] == Type.landGood )
                    {
                        return true;
                    }
                    if ( points[ pos.Item1, pos.Item2 + 1 ] == type )
                    {
                        queue.Enqueue( (pos.Item1, pos.Item2 + 1) );
                    }
                }
                if ( !queue.Contains( (pos.Item1, pos.Item2 - 1) ) )
                {
                    if ( points[ pos.Item1, pos.Item2 - 1 ] == Type.landGood )
                    {
                        return true;
                    }
                    if ( points[ pos.Item1, pos.Item2 - 1 ] == type )
                    {
                        queue.Enqueue( (pos.Item1, pos.Item2 - 1) );
                    }
                }
            }

            return false;
        }

        static bool CorrectDam( Lake lake, int y, int x )
        {
            int countСrossings = 0;
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
                            if ( countСrossings > 1 )
                                return false;
                        }
                        if ( lake.points[ pos.Item1 + 1, pos.Item2 ] == Type.water )
                        {
                            queue.Enqueue( (pos.Item1 + 1, pos.Item2) );
                        }
                    }
                }
                if ( !queue.Contains( (pos.Item1 - 1, pos.Item2) ) )
                {
                    if ( pos.Item1 - 1 >= 0 )
                    {
                        if ( lake.points[ pos.Item1 - 1, pos.Item2 ] == Type.newLake )
                        {
                            countСrossings++;
                            if ( countСrossings > 1 )
                                return false;
                        }
                        if ( lake.points[ pos.Item1 - 1, pos.Item2 ] == Type.water )
                        {
                            queue.Enqueue( (pos.Item1 - 1, pos.Item2) );
                        }
                    }
                }
                if ( !queue.Contains( (pos.Item1, pos.Item2 + 1) ) )
                {
                    if ( pos.Item2 + 1 < lake.points.GetLength( 1 ) )
                    {
                        if ( lake.points[ pos.Item1, pos.Item2 + 1 ] == Type.newLake )
                        {
                            countСrossings++;
                            if ( countСrossings > 1 )
                                return false;
                        }
                        if ( lake.points[ pos.Item1, pos.Item2 + 1 ] == Type.water )
                        {
                            queue.Enqueue( (pos.Item1, pos.Item2 + 1) );
                        }
                    }
                }
                if ( !queue.Contains( (pos.Item1, pos.Item2 - 1) ) )
                {
                    if ( pos.Item2 - 1 >= 0 )
                    {
                        if ( lake.points[ pos.Item1, pos.Item2 - 1 ] == Type.newLake )
                        {
                            countСrossings++;
                            if ( countСrossings > 1 )
                                return false;
                        }
                        if ( lake.points[ pos.Item1, pos.Item2 - 1 ] == Type.water )
                        {
                            queue.Enqueue( (pos.Item1, pos.Item2 - 1) );
                        }

                    }
                }
            }

            return countСrossings == 1;
        }

        #region Read
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
                        lake.points[ y, x ] = Type.landGood;
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
                FillLand( lake.points );
                lakes.Add( lake );
            }

            return lakes;
        }

        private static void FillLand( Type[,] lake )
        {
            int width = lake.GetLength( 0 );
            int height = lake.GetLength( 1 );

            int wavelength = 0;
            bool isDorw;
            Point position = new Point();
            Point adjacent = new Point();
            do
            {
                isDorw = false;
                for ( position.y = 0; position.y < width; ++position.y )
                {
                    for ( position.x = 0; position.x < height; ++position.x )
                    {
                        if ( lake[ position.y, position.x ] == Type.landGood )
                        {
                            for ( Direction direction = 0; direction <= Direction.up; direction++ )
                            {
                                SwitchDirection( direction, position, ref adjacent );

                                if ( IsNewPositionCorrect( adjacent, height, width ) && ( lake[ adjacent.y, adjacent.x ] == Type.land ) )
                                {
                                    isDorw = true;
                                    lake[ adjacent.y, adjacent.x ] = Type.landGood;
                                }
                            }
                        }

                    }
                }
                wavelength++;
            } while ( isDorw );
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

        private static void SwitchDirection( Direction direction, Point position, ref Point adjacent )
        {
            switch ( direction )
            {
                case Direction.left:
                    adjacent.x = position.x - 1;
                    adjacent.y = position.y;
                    break;
                case Direction.right:
                    adjacent.x = position.x + 1;
                    adjacent.y = position.y;
                    break;
                case Direction.down:
                    adjacent.x = position.x;
                    adjacent.y = position.y - 1;
                    break;
                case Direction.up:
                    adjacent.x = position.x;
                    adjacent.y = position.y + 1;
                    break;
            }
        }

        public struct Point
        {
            public int x;
            public int y;
        }

        public enum Direction
        {
            left,
            right,
            down,
            up
        }
        private static bool IsNewPositionCorrect( Point adjacent, int height, int width )
        {
            return adjacent.x >= 0 && adjacent.x < height && adjacent.y >= 0 && adjacent.y < width;
        }
        #endregion
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
        landGood = ',',
        newLake = 'O',
        oldLand = 'x',
        newNewLake = 'z',
        oldnewLand = 's'
    }
}
