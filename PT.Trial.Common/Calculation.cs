using System;
using System.Threading.Tasks;
using NLog;

namespace PT.Trial.Common
{
    public class Calculation
    {
        private ILogger _logger;
        public string Id { get; set; }

        public Calculation(string id)
        {
            Id = id;
            _logger = LogService.CreateLogger(Id);
        }

        public void SendStartNumber()
        {
            SendAsync(new Number(1, 1)).Wait();
        }

        public async Task HandleAsync(Number number)
        {
            if (number.Index >= 50)
            {
                _logger.Error($"Calculation #{Id}: reached MAX number count. Increase count in app settings if needed.");
                _logger.Error($"Calculation #{Id}: last received number {number}. Check logs for the full output.");
                _logger.Error("");
                return;
            }

            _logger.Trace($"Received: {number}");

            var next = CalcService.GetNextNumber(number);

            await SendAsync(next);
        }

        public async Task SendAsync(Number number)
        {
            _logger.Trace($"Sending: {number}");

            await HttpService.SendAsync(number, Id);
        }
    }
}