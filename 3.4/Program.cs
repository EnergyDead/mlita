using System;
using System.IO;

namespace _3._4
{
    internal class Program
    {
        const string inputFileName = "input.txt";
        const string outputFileName = "output.txt";
        static void Main( string[] args )
        {
            int n = int.Parse( File.ReadAllText( inputFileName ) );
            ulong ticketsCoun = Calculate( n );
            Console.WriteLine( ticketsCoun );
        }

        private static ulong Calculate( int digitsCount )
        {

         Cache cache = new Cache( digitsCount + 1, 10, digitsCount * 9 + 1 );
            if ( digitsCount == 0 )
            {
                return 0;
            }
            ulong count = 0;

            for ( int lastDigit = 0; lastDigit < 10; lastDigit++ )
            {
                var maxDigitsSum = GetMaxDigitsSum( lastDigit, digitsCount );
                for ( int digitsSum = GetMinDigitsSum( lastDigit, digitsCount ); digitsSum <= maxDigitsSum; digitsSum++ )
                {
                    var numbersCount = f( digitsCount, lastDigit, digitsSum, cache ); 
                    count += numbersCount * ( numbersCount + ( lastDigit == 0 ? 0 : f( digitsCount, lastDigit - 1, digitsSum, cache ) ) + ( lastDigit == 9 ? 0 : f( digitsCount, lastDigit + 1, digitsSum, cache ) ) );

                }
            }

            return count;
        }

        private static ulong f( int digitsCount, int lastDigit, int digitsSum, Cache cache )
        {
            {
                if ( digitsSum < 0 )
                {
                    return 0;
                }
                if ( digitsSum == 0 && digitsSum > 0 )
                {
                    return 0;
                }
                if ( digitsCount == 1 )
                {
                    return (ulong)(lastDigit == digitsSum && inRange( lastDigit, 0, 9 ) ? 1 : 0);
                }
                if ( lastDigit == digitsSum && digitsSum == 0 )
                {
                    return 1;
                }

                ulong? item = cache.get( digitsCount, lastDigit, digitsSum );
                item = item ?? f( digitsCount - 1, lastDigit, digitsSum - lastDigit, cache ) + ( lastDigit == 0 ? 0 : f( digitsCount - 1, lastDigit - 1, digitsSum - lastDigit, cache ) ) + ( lastDigit == 9 ? 0 : f( digitsCount - 1, lastDigit + 1, digitsSum - lastDigit, cache ) );
                cache.set( digitsCount, lastDigit, digitsSum, item );

                return item.Value;
            }
        }

        private static bool inRange( int digit, int min, int max )
        {
            return min <= digit && digit <= max;
        }

        private static int GetMinDigitsSum( int lastDigit, int digitsCount )
        {
            var progressiveCount = Math.Min( digitsCount, lastDigit );
            return ( ( 2 * ( lastDigit - progressiveCount + 1 ) + ( progressiveCount - 1 ) ) * progressiveCount ) / 2;
        }

        private static int GetMaxDigitsSum( int lastDigit, int digitsCount )
        {
            var progressiveCount = Math.Min( digitsCount, 10 - lastDigit );

            return ( ( 2 * lastDigit + ( progressiveCount - 1 ) ) * progressiveCount ) / 2 + ( digitsCount - progressiveCount ) * 9;
        }

        private struct Cache
        {
            private ulong?[] items;
            private int maxX;
            private int maxY;
            private int maxZ;

            public Cache( int mX, int mY, int mZ )
            {
                maxX = mX;
                maxY = mY;
                maxZ = mZ;
                items = new ulong?[ mX * mY * mZ ];
            }

            public ulong? get( int x, int y, int z )
            {
                var pos = getPos( x, y, z );
                if ( pos >= items.Length )
                {
                    throw new Exception( "Invalid point" );
                }

                return items[ pos ];
            }

            public void set( int x, int y, int z, ulong? v )
            {
                var pos = getPos( x, y, z );
                if ( pos >= items.Length )
                {
                    throw new Exception( "Invalid point" );
                }

                items[ pos ] = v;
            }

            private int getPos( int x, int y, int z )
            {
                return z * maxY * maxX + y * maxX + x;
            }
        }
    }
}
