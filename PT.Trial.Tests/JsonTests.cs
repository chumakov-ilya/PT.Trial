using System;
using Newtonsoft.Json;
using NUnit.Framework;
using PT.Trial.Common;

namespace PT.Trial.Tests
{
    public class JsonTests
    {
        [Test]
        public void Serialization_LongMax_Correct()
        {
            var x = new Number(0, long.MaxValue);

            string serialized = JsonConvert.SerializeObject(x);

            var deserialized = JsonConvert.DeserializeObject<Number>(serialized);

            Assert.AreEqual(long.MaxValue.ToString(), deserialized.Value);
        }
    }
}