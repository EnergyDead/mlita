using System;
using System.Collections.Generic;
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

            foreach ( var lake in lakes )
            {
                result.Add( SearchDam( lake ) );
            }
            File.WriteAllText( outputFileName, String.Join( " ", result ) );
        }

        private static int SearchDam( Lake lake )
        {
            if ( lake.points.Length == 2 )
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

                                    if ( CountLend2( points, y, x + 1 ) )
                                    {
                                        return 1;
                                    }

                                    points.points[ y, x + 1 ] = Type.land;

                                    if ( HasMoreLand( points, y, x ) > 1 )
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

                                    if ( CountLend( points, y + 1, x ) )
                                    {
                                        return 1;
                                    }
                                    points.points[ y + 1, x ] = Type.land;
                                    if ( HasMoreLand( points, y, x ) > 1 )
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

        private static bool CountLend( Lake points, int y, int x )
        {
            var a = ( y + 1 < points.points.GetLength( 0 ) && points.points[ y + 1, x ] == Type.land );
            var b = ( x + 1 < points.points.GetLength( 1 ) && points.points[ y, x + 1 ] == Type.land );
            var c = ( x - 1 > 0 && points.points[ y, x - 1 ] == Type.land );
            return a && b && c;
        }

        private static bool CountLend2( Lake points, int y, int x )
        {
            var a = ( x + 1 < points.points.GetLength( 1 ) && points.points[ y, x + 1 ] == Type.land );
            var b = ( y + 1 < points.points.GetLength( 0 ) && points.points[ y + 1, x ] == Type.land );
            var c = ( y - 1 > 0 && points.points[ y - 1, x ] == Type.land );
            return a && b && c;
        }

        private static int HasMoreLand( Lake points, int y, int x )
        {
            var f = points.points;
            int count = 0;
            for ( int i = y; i < f.GetLength( 0 ); ++i )
            {
                for ( int j = x; j < f.GetLength( 1 ); ++j )
                {
                    if ( f[ i, j ] == Type.water )
                    {
                        dfs( f, i, j );
                        count++;
                    }
                }
            }
            return count;
        }

        static void dfs( Type[,] f, int i, int j )
        {
            if ( f[ i, j ] == Type.water )
            {
                f[ i, j ] = Type.land;
                if ( i + 1 < f.GetLength( 0 ) )
                    dfs( f, i + 1, j );
                if ( i - 1 >= 0 )
                    dfs( f, i - 1, j );
                if ( j + 1 < f.GetLength( 1 ) )
                    dfs( f, i, j + 1 );
                if ( j - 1 >= 0 )
                    dfs( f, i, j - 1 );
            }
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
        land = '.'
    }
}
