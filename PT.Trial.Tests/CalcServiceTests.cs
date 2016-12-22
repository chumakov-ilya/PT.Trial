using NUnit.Framework;
using PT.Trial.Common;

namespace PT.Trial.Tests
{
    public class CalcServiceTests
    {
        [TestCase(1, "1", "0")]
        [TestCase(2, "1", "1")]
        [TestCase(3, "2", "1")]
        [TestCase(4, "3", "2")]
        [TestCase(5, "5", "3")]
        [TestCase(20, "6765", "4181")]
        [TestCase(50, "12586269025", "7778742049")]
        [TestCase(90, "2880067194370816120", "1779979416004714189")]
        public void GetPrevNumber_Default_ReturnsCorrectNumber(long currentIndex, string currentValue, string prevValue)
        {
            var current = new Number(currentIndex, currentValue);

            var prev = CalcService.GetPrevNumber(current);

            Assert.AreEqual(current.Index - 1, prev.Index);
            Assert.AreEqual(prevValue, prev.Value);
        }

        [TestCase(89, "1779979416004714189", "2880067194370816120")]
        public void GetNextNumber_Default_ReturnsCorrectNumber(long currentIndex, string currentValue, string nextValue)
        {
            var current = new Number(currentIndex, currentValue);

            var next = CalcService.GetNextNumber(current);

            Assert.AreEqual(current.Index + 1, next.Index);
            Assert.AreEqual(nextValue, next.Value);
        }
    }
}