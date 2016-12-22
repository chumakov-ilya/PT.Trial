using System;
using EasyNetQ;

namespace PT.Trial.Common.Contracts
{
    public interface IBusService
    {
        void Send(Number number, string calculationId);
        IBus CreateBus();
        void Receive(IBus bus, Calculation calculation, Action<Number> onMessage);
    }
}