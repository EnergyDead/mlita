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
            List<Lake> lakes = ReadLakes();
            List<int> result = new List<int>( lakes.Count );
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach ( var lake in lakes )
            {

                // Print( lake.points );
                result.Add( MinDamLength( lake ) );
            }
            stopWatch.Stop();
            Console.WriteLine( stopWatch.ElapsedMilliseconds );
            Console.WriteLine( String.Join( " ", result ) );
            File.WriteAllText( outputFileName, String.Join( " ", result ) );
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

                                    if ( LikeSizeIsOne( points, y, x + 1 ) )
                                    {
                                        return 1;
                                    }
                                    points.points[ y, x + 1 ] = Type.newLake;

                                    var first = CountLandQueue( points, y, x );
                                    if ( first == 1 )
                                    {
                                        return 1;
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

                                    if ( LikeSizeIsOneReverse( points, y + 1, x ) )
                                    {
                                        return 1;
                                    }
                                    points.points[ y + 1, x ] = Type.newLake;
                                    var first = CountLandQueue( points, y, x );
                                    if ( first == 1 )
                                    {
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

        static int CountLandQueue( Lake lake, int y, int x )
        {
            int countСrossings = 0;
            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue( (y, x) );
            while ( queue.Count > 0 && countСrossings < 2 )
            {
                var pos = queue.Dequeue();
                lake.points[ pos.Item1, pos.Item2 ] = Type.land;
                // Print( lake.points );
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
                    }
                }
            }

            return countСrossings;
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
        newLake = 'O'
    }
}
