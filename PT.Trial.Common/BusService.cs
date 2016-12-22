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
                bus.Publish(number, calculationId);
            }
        }

        public static void Subscribe(Number number, Action<Number> onMessage)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                bus.Subscribe<Number>("test", onMessage);
            }
        }
    }
}