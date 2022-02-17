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
        static void Main( string[] args )
        {
            string[] input = File.ReadAllLines( inputFileName );
            int n = int.Parse( input[ 0 ] );
            List<int> fiel = input[ 1 ].Split( " " ).Select( ch => int.Parse( ch ) ).ToList();

            List<int> next = Enumerable.Repeat( 0, n + 1 ).ToList();

            List<int> last = Enumerable.Repeat( int.MaxValue / 2, n + 1 ).ToList();
            for ( int i = n - 1; i > 0; i-- )
            {
                next[ i ] = last[ fiel[ i ] ];
                last[ fiel[ i ] ] = i;
            }

            List<int> minJumps = Enumerable.Repeat( int.MaxValue / 2, n + 1 ).ToList();
            List<int> jumpTo = Enumerable.Repeat( 0, n + 1 ).ToList();
            List<Pair> tree = Enumerable.Repeat( new Pair { minJump = int.MaxValue / 2, where = 0 }, 2 + 2 * n ).ToList();
            minJumps[ n ] = 0;
            Update( tree, n, new Pair { minJump = 0, where = n } );
            for ( int i = n - 1; i >= 1; i-- )
            {
                if ( next[ i ] == 0 )
                {
                    Update( tree, i, new Pair { minJump = int.MaxValue / 2, where = i } );
                }
                else
                {
                    Pair pair = GetMin( tree, i + 1, next[ i ] );
                    minJumps[ i ] = pair.minJump;
                    jumpTo[ i ] = pair.where;
                }
                Update( tree, i, new Pair { minJump = minJumps[ i ], where = i } );
            }
            if ( minJumps[ 1 ] == int.MaxValue / 2 )
            {
                Console.WriteLine( "0" );
            }
            else
            {
                Console.WriteLine( minJumps[ 1 ] );
                for ( int i = 1; i < n; i = jumpTo[ i ] )
                {
                    if ( i > 1 )
                    {
                        Console.Write( " " );
                    }
                    Console.WriteLine( i );
                }
            }

            Console.WriteLine( "Hello World!" );
        }

        bool Checker( Pair left, Pair right )
        {
            return left.minJump < right.minJump;
        }

        static void Update( List<Pair> tree, int index, Pair value )
        {
            index += tree.Count / 2;
            tree[ index ] = value;

            // tree[index] = min(tree[index * 2], tree[index * 2 + 1];

            while ( index > 1 )
            {
                index >>= 1;
                tree[ index ] = Min( tree[ index * 2 ], tree[ index * 2 + 1 ] );
            }
        }

        static Pair GetMin( List<Pair> tree, int left, int right )
        {
            left += tree.Count >> 1;
            right += tree.Count >> 1;
            Pair result = new Pair() { minJump = int.MaxValue / 2, where = 0 };

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
                    result = Min( result, tree[ left ] );
                    right--;
                    right /= 2;
                }
            }

            return result;
        }

        static Pair Min( Pair left, Pair right )
        {
            if ( left.minJump < right.minJump )
            {
                return left;
            }
            return right;
        }
    }

    struct Pair
    {
        public int minJump;
        public int where;
    }
}
