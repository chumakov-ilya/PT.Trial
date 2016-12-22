using System;
using EasyNetQ;
using PT.Trial.Common.Contracts;

namespace PT.Trial.Common.Services
{
    public class BusService : IBusService
    {
        public AppSettings Settings { get; }

        public BusService(AppSettings settings)
        {
            Settings = settings;
        }

        public void Send(Number number, string calculationId)
        {
            using (var bus = RabbitHutch.CreateBus(Settings.BusConnectionString))
            {
                bus.Send(GetQueueName(calculationId), number);
            }
        }

        private static string GetQueueName(string calculationId)
        {
            if (string.IsNullOrEmpty(calculationId)) throw  new ArgumentNullException(nameof(calculationId));

            return "queue." + calculationId;
        }

        public IBus CreateBus()
        {
            return RabbitHutch.CreateBus(Settings.BusConnectionString);
        }

        public void Receive(IBus bus, Calculation calculation, Action<Number> onMessage)
        {
            bus.Receive<Number>(GetQueueName(calculation.Id), async x => await calculation.HandleAsync(x));
        }
    }
}