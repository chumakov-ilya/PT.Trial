using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PT.Trial.Common;

namespace PT.Trial.Tests
{
    public class CalculatorTests
    {
        [TestCase(0, ExpectedResult = "0")]
        [TestCase(1, ExpectedResult = "1")]
        [TestCase(2, ExpectedResult = "1")]
        [TestCase(3, ExpectedResult = "2")]
        [TestCase(4, ExpectedResult = "3")]
        [TestCase(5, ExpectedResult = "5")]
        [TestCase(200, ExpectedResult = "280571172992510140037611932413038677189525")]
        [TestCase(500, ExpectedResult = "139423224561697880139724382870407283950070256587697307264108962948325571622863290691557658876222521294125")]
        public string GetValueByIndex_SupportedIndex_ReturnsCorrectValue(long index)
        {
            return Calculator.GetValueByIndex(index).ToString();
        }
    }
}
