using System.Collections.Concurrent;
using System.Numerics;

namespace PT.Trial.Common
{
    public static class CalcService
    {
        public static ConcurrentDictionary<long, Number> Cache { get; } = new ConcurrentDictionary<long, Number>();

        public static Number GetNextNumber(Number current)
        {
            Number prev = GetPrevNumber(current);

            string sum = (BigInteger.Parse(prev.Value) + BigInteger.Parse(current.Value)).ToString();

            var next = new Number(current.Index + 1, sum);

            Cache.TryAdd(next);

            return next;
        }

        public static Number GetPrevNumber(Number current)
        {
            long prevIndex = current.Index - 1;

            Number prev;
            bool cached = Cache.TryGetValue(prevIndex, out prev);

            return cached ? prev : Calculator.GetNumberByIndex(prevIndex);
        }
    }
}