using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace _2._4
{
    internal class Program
    {
        const string inputFileName = "input.txt";
        const string outputFileName = "output.txt";
        const int INF = int.MaxValue / 2;
        static void Main( string[] args )
        {
            string[] input = File.ReadAllLines( inputFileName );
            int n = int.Parse( input[ 0 ] );
            List<int> fiel = input[ 1 ].Split( " " ).Select( ch => int.Parse( ch ) ).ToList();
            int Max = fiel.Max();
            List<int> nearest = Enumerable.Repeat( 0, n ).ToList();
            List<int> pos = Enumerable.Repeat( -1, Max + 1 ).ToList();

            for ( int i = n - 1; i >= 0; i-- )
            {
                nearest[ i ] = pos[ fiel[ i ] ];
                pos[ fiel[ i ] ] = i;
            }
            List<int> dist = Enumerable.Repeat( INF, n ).ToList();
            List<int> from = Enumerable.Repeat( -1, n ).ToList();
            List<Pair> queue = new List<Pair>();
            queue.Add( new Pair() );
            for ( int i = 0; i < n; i++ )
            {
                queue = queue.OrderBy( p => p.value ).ToList();
                queue.Reverse();
                while ( queue.Count > 0 && queue[ 0 ].time < i )
                {
                    queue.Remove( queue[ 0 ] );
                }
                if ( queue.Count == 0 )
                {
                    Console.WriteLine( "0" );
                    return;
                }
                dist[ i ] = queue[ 0 ].value;
                from[ i ] = queue[ 0 ].from;
                queue.Add( new Pair()
                {
                    value = dist[ i ] + 1,
                    time = nearest[ i ],
                    from = i
                } );
            }
            List<int> result = new List<int>();
            for ( int i = dist[ n - 1 ], j = from[ n - 1 ]; i > 0; i-- )
            {
                result.Add( j + 1 );
                j = from[ j ];

            }
            result.Reverse();

            Console.WriteLine( string.Join( " ", result.Select( ch => ch.ToString() ) ) );
            Console.WriteLine( "Hello World!" );
        }

        public struct Pair
        {
            public int value;
            public int time;
            public int from;
        }
    }
}