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

            Assert.AreEqual(long.MaxValue, deserialized.Value);
        }

        [Test]
        public void Serialization_Message_Correct()
        {
            var x = new Number(0, long.MaxValue);

            var message = new Message<Number>(x, 1);

            string serialized = JsonConvert.SerializeObject(message);

            var deserialized = JsonConvert.DeserializeObject<Message<Number>>(serialized);

            Assert.AreEqual(long.MaxValue, deserialized.Payload.Value);
        }
    }
}