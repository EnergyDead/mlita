using System;
using System.IO;
using System.Linq;

namespace maxNumber
{
    internal class Program
    {
        const string inputFileName = "input.txt";
        const string outputFileName = "output.txt";
        static void Main( string[] args )
        {
            string[] input = File.ReadAllLines( inputFileName );
            string number = input[ 1 ];
            var it = number.Min();
            File.WriteAllText( outputFileName, ( number.IndexOf( it ) + 1 ).ToString() );
        }
    }
}
