using System.Collections.Concurrent;

namespace PT.Trial.Common
{
    public static class CalcService
    {
        public static ConcurrentDictionary<long, Number> Cache { get; } = new ConcurrentDictionary<long, Number>();

        public static Number GetNextNumber(Number current)
        {
            Number prev = GetPrevNumber(current);

            var next = new Number(current.Index + 1, prev.Value + current.Value);

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