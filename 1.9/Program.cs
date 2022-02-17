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
            List<int> result = new List<int>();

            foreach ( var lake in lakes )
            {
                result.Add( SearchDam( lake ) );
            }
            Console.WriteLine( String.Join(" ", result ) );
        }

        private static int SearchDam( Lake lake )
        {
            List<int> result = new List<int>();
            for ( int x = 0; x < lake.width; x++ )
            {
                for ( int y = 0; y < lake.height; y++ )
                {
                    Point point = new Point( x, y, (char)lake.points[ y, x ] );
                    if ( point.type == Type.water )
                    {
                        if ( lake.points[ y, x - 1 ] == Type.water && lake.points[ y, x + 1 ] == Type.water && lake.points[ y - 1, x ] == Type.land && lake.points[ y + 1, x ] == Type.land )
                        {
                            result.Add( 1 );
                        }

                        if ( lake.points[ y, x - 1 ] == Type.water && lake.points[ y, x + 1 ] == Type.water )
                        {
                            if ( lake.points[ y - 1, x ] == Type.land && lake.points[ y + 1, x ] == Type.water )
                            {
                                int count = 0;
                                int Ynext = y;
                                while ( lake.points[ Ynext, x ] == Type.water )
                                {
                                    count++;
                                    Ynext++;
                                }
                                result.Add( count );
                            }

                            if ( lake.points[ y + 1, x ] == Type.land && lake.points[ y - 1, x ] == Type.water )
                            {
                                int count = 0;
                                int Ynext = y;
                                while ( lake.points[ Ynext, x ] == Type.water )
                                {
                                    count++;
                                    Ynext--;
                                }
                                result.Add( count );
                            }
                        }

                        if ( lake.points[ y - 1, x ] == Type.water && lake.points[ y + 1, x ] == Type.water )
                        {
                            if ( lake.points[ y, x - 1 ] == Type.land && lake.points[ y, x + 1 ] == Type.water )
                            {
                                int count = 0;
                                int Xnext = x;
                                while ( lake.points[ y, Xnext ] == Type.water )
                                {
                                    count++;
                                    Xnext++;
                                }
                                result.Add( count );
                            }

                            if ( lake.points[ y + 1, x ] == Type.land && lake.points[ y - 1, x ] == Type.water )
                            {
                                int count = 0;
                                int Xnext = x;
                                while ( lake.points[ y, Xnext ] == Type.water )
                                {
                                    count++;
                                    Xnext--;
                                }
                                result.Add( count );
                            }
                        }
                    }
                }
            }

            return result.Min();
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
                Lake lake = new Lake( size.First(), size.Last() );
                for ( int y = 0; y < lake.height; y++ )
                {
                    var line = input.Dequeue();
                    for ( int x = 0; x < line.Length; x++ )
                    {
                        lake.points[ y, x ] = (Type)line[ x ];
                    }
                }
                lakes.Add( lake );
            }

            return lakes;
        }
    }
    struct Lake
    {
        public Lake( int h, int w )
        {
            width = w;
            height = h;
            points = new Type[ h, w ];
        }

        /// <summary>
        /// ширина
        /// </summary>
        public int width;

        /// <summary>
        /// высота
        /// </summary>
        public int height;
        // public List<Point> points;
        public Type[,] points;
    }
    struct Point
    {
        public Point( int x, int y, char type )
        {
            this.x = x;
            this.y = y;
            this.type = (Type)type;
        }
        public int x, y;
        public Type type;
    }
    enum Type
    {
        water = '#',
        land = '.'
    }
}
