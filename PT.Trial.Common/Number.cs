using System.Numerics;

namespace PT.Trial.Common
{
    public class Number
    {
        public long Index { get; set; }
        public string Value { get; set; }

        public Number() {}

        public Number(long index, string value)
        {
            Index = index;
            Value = value;
        }

        public Number(long index, BigInteger value) : this(index, value.ToString()) {}

        public override string ToString()
        {
            return $"N({Index}) = {Value}";
        }
    }
}

