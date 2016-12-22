using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using NLog;
using PT.Trial.Common.Contracts;

namespace PT.Trial.Common
{
    public class Calculation
    {
        private ILogger _logger;

        public string Id { get;  }

        public IBusService BusService { get; }
        public ICalcService CalcService { get; }
        public ILogService LogService { get; }
        public IHttpService HttpService { get; }
        public AppSettings Settings { get; }

        public Calculation(
            string id, 
            IBusService busService, 
            ICalcService calcService,
            ILogService logService, 
            IHttpService httpService, 
            AppSettings settings)
        {
            Id = id;

            BusService = busService;
            CalcService = calcService;
            LogService = logService;
            HttpService = httpService;
            Settings = settings;

            _logger = LogService.CreateLogger(Id);
        }

        public void SubscribeTo(IBus bus)
        {
            BusService.Receive(bus, this, async x => await HandleAsync(x));
        }

        public async Task HandleAsync(Number number)
        {
            if (number.Index >= Settings.MaxNumberCount)
            {
                _logger.Error($"Calculation #{Id}: reached MAX number count ({Settings.MaxNumberCount}). Increase count in app settings if you are REALLY SURE.");
                _logger.Error($"Calculation #{Id}: last received number {number}. Check logs for the full output.");
                _logger.Error("");
                return;
            }

            _logger.Trace($"Received: {number}");

            var next = CalcService.GetNextNumber(number);

            await SendAsync(next);
        }

        public void SendStartNumber()
        {
            SendAsync(new Number(1, 1)).Wait();
        }

        public async Task SendAsync(Number number)
        {
            try
            {
                _logger.Trace($"Sending: {number}");

                await HttpService.SendAsync(number, Id);
            }
            catch (HttpRequestException)
            {
                _logger.Fatal($"No connection with second (REST) app  {Settings.WebConnectionString}");
            }
        }
    }
}