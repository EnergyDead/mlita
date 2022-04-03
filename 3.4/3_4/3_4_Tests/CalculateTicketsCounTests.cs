using _3_4;
using System;
using System.Collections.Generic;
using Xunit;

namespace _3_4_Tests
{
    public class CalculateTicketsCounTests
    {
        [Fact]
        public void TestFull()
        {
            int count = 10;
            var resultCorrect = new ulong[] { 10, 46, 220, 1296, 8048, 53638, 371078, 2650538, 19375268, 144325910, 1091464370, 8358438408, 64685063442, 505079547448, 3974118727560, 31477515674650, 250767275805968, 2007919397824778, 16149898166296070, 130413870249898164, 1056872323104935290, 8592240655991616634, 14714515422084515586, 809908325018698526, 6734637416941051746, 12322883897414648822, 5007310517262000690, 2191914238077493904, 1295559759242882916, 9599604908712864390 };
            var results = new List<ulong>();

            for(int i = 1; i <= count; i++)
            {
                results.Add(Program.CalculateTicketsCoun(i));
                Assert.Equal( resultCorrect[i-1], results[i-1] );
            }
        }
    }
}
