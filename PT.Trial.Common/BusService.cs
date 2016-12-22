using System;
using EasyNetQ;

namespace PT.Trial.Common
{
    public static class BusService
    {
        public static void Publish(Number number, string calculationId)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                bus.Publish(number, "topic" + calculationId);
            }
        }

        public static IBus CreateBus()
        {
            return RabbitHutch.CreateBus("host=localhost");
        }

        public static void Subscribe(IBus bus, Calculation calculation, Action<Number> onMessage)
        {
            bus.Subscribe<Number>("test",
                async x => await calculation.HandleAsync(x),
                x => x.WithTopic("topic" + calculation.Id));
        }
    }
}