using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _4._8
{
    internal class Program
    {

        static int MAX_N = 100000;
        static int MIN_N = 2;
        const string inputFileName = "input.txt";
        const string outputFileName = "output.txt";
        static void Main( string[] args )
        {
            var input = File.ReadAllLines( inputFileName );
            int N = int.Parse( input[ 0 ] );
            string boolean = input[ 1 ];
            List<List<int>> dynamice = fill();
            List<List<int>> method = Enumerable.Repeat( Enumerable.Repeat( 0, MIN_N ).ToList(), MAX_N ).ToList();
            List<List<int>> result = Enumerable.Repeat( Enumerable.Repeat( 0, MIN_N ).ToList(), MAX_N ).ToList();
            dynamice[ 1 ][ 0 ] = 0;
            dynamice[ 1 ][ 1 ] = 1;
            result[ 1 ][ 0 ] = 0;
            result[ 1 ][ 1 ] = 1;
            for ( int i = MIN_N; i <= N; ++i )
            {
                dynamice[ i ][ 0 ] = -1;
                dynamice[ i ][ 1 ] = -1;
                for ( var j = 0; j <= 1; ++j )
                {
                    for ( var k = 0; k <= 1; ++k )
                    {
                        int b = ToInt( boolean[ j * 2 + k ] );
                        if ( ( dynamice[ i - 1 ][ j ] >= 0 ) && ( dynamice[ i ][ b ] < dynamice[ i - 1 ][ j ] + k ) )
                        {
                            dynamice[ i ][ b ] = dynamice[ i - 1 ][ j ] + k;
                            method[ i ][ b ] = j;
                            result[ i ][ b ] = k;
                        }
                    }
                }
            }

            if ( dynamice[ N ][ 1 ] < 0 )
            {
                File.WriteAllText( outputFileName, "No" );
                return;
            }
            List<int> buffer = new List<int>();
            for ( int i = 0; i < MAX_N; i++ )
            {
                buffer.Add( 0 );
            }
            int z = 1;
            for ( int i = N; i >= 1; --i )
            {
                buffer[ i ] = result[ i ][ z ];
                z = method[ i ][ z ];
            }
            string message = string.Empty;
            for ( int i = 0; i <= N; i++ )
            {
                message += buffer[ i ].ToString();
            }
            Console.WriteLine(message);
        }

        private static List<List<int>> fill()
        {
            List<List<int>> result = new List<List<int>>();
            for ( int i = 0; i < MAX_N; i++ )
            {
                result.Add( new List<int>() );
                for ( int j = 0; j < MIN_N; j++ )
                {
                    result[ i ].Add( 0 );
                }
            }

            return result;
        }

        private static int ToInt( char value )
        {
            return value - '0';
        }
    }
}
