/*
        14.10. Распил бруса 2 (10)
    На пилораму привезли брус длиной L метров. Требуется сделать N распилов. Распилы делят
    брус на части, длина которых выражается натуральными числами. Стоимость одного распила
    равна длине распиливаемого бруса. Определить минимальную стоимость распила.

    Visual Studio 2019    
    Васильев Руслан
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _14._10
{
    internal class Program
    {
        const string inputFileName = "input.txt";
        const string outputFileName = "output.txt";
        static void Main( string[] args )
        {
            string[] input = File.ReadAllLines( inputFileName );

            List<string> tokens = input[ 0 ].Split( " " ).ToList();
            (int, int) param = (int.Parse( tokens[ 0 ] ), int.Parse( tokens[ 1 ] ));

            List<int> parts = new List<int>();
            PriorityQueue queue = new PriorityQueue();
            for ( int i = 0; i < param.Item2; i++ )
            {
                parts.Add( 1 );
                queue.Enqueue( 1 );
            }
            parts.Add( param.Item1 - param.Item2 );
            queue.Enqueue( param.Item1 - param.Item2 );

            while ( !queue.IsEmpty() )
            {
                int left = queue.Dequeue().Value;
                if ( queue.IsEmpty() )
                { break; }
                int right = queue.Dequeue().Value;
                queue.Enqueue( left + right );
                parts.Add( left + right );
            }
            var sum = 0;

            foreach ( var part in parts )
            {
                if ( part == param.Item1 )
                {
                    continue;
                }
                sum += part;
            }
            Console.WriteLine( sum );
            File.WriteAllText( outputFileName, sum.ToString() );
        }
    }
}
