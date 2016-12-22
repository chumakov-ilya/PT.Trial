using System;
using EasyNetQ;
using PT.Trial.Common.Contracts;

namespace PT.Trial.Common.Services
{
    public class BusService : IBusService
    {
        public void Publish(Number number, string calculationId)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                bus.Publish(number, GetTopicId(calculationId));
            }
        }

        private static string GetTopicId(string calculationId)
        {
            if (string.IsNullOrEmpty(calculationId)) throw  new ArgumentNullException(nameof(calculationId));

            return "topic" + calculationId;
        }

        public IBus CreateBus()
        {
            return RabbitHutch.CreateBus("host=localhost");
        }

        public void Subscribe(IBus bus, Calculation calculation, Action<Number> onMessage)
        {
            bus.Subscribe<Number>("test",
                async x => await calculation.HandleAsync(x),
                x => x.WithTopic(GetTopicId(calculation.Id)));
        }
    }
}