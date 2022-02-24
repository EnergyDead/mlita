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
            List<int> fiel = input[ 1 ].Split( " " ).Select( ch => int.Parse( ch ) ).ToList();

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
            if ( segmentLength[ 1 ] != MAX )
            {
                var res = new List<int>();
                for ( int i = 1; i < countNumber; i = next[ i ] )
                {
                    res.Add( i );
                }
                result = String.Join( " ", res );
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