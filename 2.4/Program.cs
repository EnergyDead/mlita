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

        static void Main( string[] args )
        {
            string[] input = File.ReadAllLines( inputFileName );
            int n = int.Parse( input[ 0 ] );
            List<int> fiel = input[ 1 ].Split( " " ).Select( ch => int.Parse( ch ) ).ToList();

            List<int> next = Enumerable.Repeat( 0, n + 1 ).ToList();
            List<int> last = Enumerable.Repeat( 0, 1 + 300 * 1000 ).ToList();

            for ( int i = n; i >= 1; i-- )
            {
                next[ i ] = last[ fiel[ i - 1 ] ];
                last[ fiel[ i - 1 ] ] = i;
            }
            List<int> result = new List<int>();
            int from = 1;
            result.Add( from );
            int rangeFirst = 2;
            while ( true )
            {
                if ( next[ from ] == n )
                {
                    Console.WriteLine( string.Join( " ", result.Select( ch => ch.ToString() ) ) );
                    return;
                }
                if ( next[ from ] == 0 )
                {
                    Console.WriteLine( "0" );
                    return;
                }
                int rangeLast = next[ from ];
                int maxNext = 0;
                int maxNextIndex = 0;
                for ( int i = rangeFirst; i <= rangeLast; i++ )
                {
                    if ( next[ i ] > maxNext )
                    {
                        maxNext = next[ i ];
                        maxNextIndex = i;
                    }
                }
                if ( maxNext <= rangeLast )
                {
                    Console.WriteLine( "0" );
                    return;
                }
                rangeFirst = rangeLast + 1;
                from = maxNextIndex;
                result.Add( from );
            }
        }
    }
}