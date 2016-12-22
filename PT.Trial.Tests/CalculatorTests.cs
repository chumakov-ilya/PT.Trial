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
        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 1)]
        [TestCase(3, ExpectedResult = 2)]
        [TestCase(4, ExpectedResult = 3)]
        [TestCase(5, ExpectedResult = 5)]
        [TestCase(92, ExpectedResult = 7540113804746346429)]
        public long GetValueByIndex_SupportedIndex_ReturnsCorrectValue(long index)
        {
            return Calculator.GetValueByIndex(index);
        }

        [TestCase(93)]
        public void GetValueByIndex_TooLong_OverflowException(long index)
        {
            Assert.Throws<OverflowException>(() => Calculator.GetValueByIndex(index));
        }
    }
}
