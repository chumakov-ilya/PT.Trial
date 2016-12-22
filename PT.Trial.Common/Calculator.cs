using System.Numerics;

namespace PT.Trial.Common
{
    public static class Calculator
    {
        public static BigInteger GetValueByIndex(long index)
        {
            BigInteger prev = 0, current = 1;

            for (long i = 2; i <= index; i++)
            {
                BigInteger tmp = prev;
                prev = current;
                current = tmp + current;
            }

            return index > 0 ? current : 0;
        }

        public static Number GetNumberByIndex(long prevIndex)
        {
            BigInteger value = GetValueByIndex(prevIndex);

            return new Number(prevIndex, value);
        }
    }
}