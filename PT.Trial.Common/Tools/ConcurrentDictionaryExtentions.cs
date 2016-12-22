using System.Collections.Concurrent;

namespace PT.Trial.Common.Tools
{
    public static class ConcurrentDictionaryExtentions
    {
        public static bool TryAdd(this ConcurrentDictionary<long, Number> dict, Number number)
        {
            return dict.TryAdd(number.Index, number);
        }
    }
}