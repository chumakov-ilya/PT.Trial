namespace PT.Trial.Common
{
    public static class Calculator
    {
        public static long GetValueByIndex(long index)
        {
            long prev = 0, current = 1;

            checked
            {
                for (long i = 2; i <= index; i++)
                {
                    long tmp = prev;
                    prev = current;
                    current = tmp + current;
                }
            }

            return index > 0 ? current : 0;
        }

        public static Number GetNumberByIndex(long prevIndex)
        {
            return new Number(prevIndex, Calculator.GetValueByIndex(prevIndex));
        }
    }
}