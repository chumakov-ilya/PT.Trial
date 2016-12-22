using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT.Trial.Common
{
    public class Number
    {
        public long Index { get; set; }
        public long Value { get; set; }

        public Number(long index, long value)
        {
            Index = index;
            Value = value;
        }

        public override string ToString()
        {
            return $"N({Index}): {Value}";
        }
    }
}

