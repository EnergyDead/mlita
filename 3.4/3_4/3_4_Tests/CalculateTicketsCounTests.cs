using _3_4;
using System;
using System.Collections.Generic;
using System.Numerics;
using Xunit;

namespace _3_4_Tests
{
    public class CalculateTicketsCounTests
    {
        [Fact]
        public void TestFull()
        {
            int count = 30;
            string[] resultCorrect = new string[] { "10", "46", "220", "1296", "8048", "53638", "371078", "2650538", "19375268", "144325910", "1091464370", "8358438408", "64685063442", "505079547448", "3974118727560", "31477515674650", "250767275805968", "2007919397824778", "16149898166296070", "130413870249898164", "1056872323104935290", "8592240655991616634", "70054747643213170434", "572658974610014798622", "4692207632139167162210", "38529124509802958423030", "316993857473142196970034", "2612706788794090420626064", "21569742247460190528368484", "178343352665157738250971782" };
            var results = new List<BigInteger>();
            for(int i = 1; i <= count; i++)
            {
                results.Add(Program.CalculateTicketsCoun(i));
                Assert.Equal( resultCorrect[i-1], results[i-1].ToString() );
            }
        }
    }
}
