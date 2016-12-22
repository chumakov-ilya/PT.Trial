using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyNetQ;
using PT.Trial.Common;

namespace PT.Trial.FirstApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = RabbitHutch.CreateBus("host=localhost");

            try
            {
                int count = ParseTaskCount(args);

                var tasks = new Task[count];

                for (int index = 0; index < count; index++)
                {
                    string calculationId = index.ToString();

                    var task = Task.Factory.StartNew(() =>
                    {
                        bus.Subscribe<Number>("test", 
                            async x => await HandleNumber(x, calculationId),
                            x => x.WithTopic(calculationId));

                        SendNumber(new Number(1, 1), calculationId).Wait();
                    });

                    tasks[index] = task;
                }

                Task.WaitAll(tasks);

                Console.WriteLine($"{count} parallel calculations are runned. Hit <return> to quit.");

                Console.ReadLine();
            }
            finally
            {
                bus.Dispose();
            }
        }

        private static int ParseTaskCount(string[] args)
        {
            const int defaultCount = 2;
            if (args == null || args.Length == 0) return defaultCount;

            int taskCount;
            bool isParsed = int.TryParse(args[1], out taskCount);

            if (!isParsed) return defaultCount;

            return taskCount;
        }

        static async Task HandleNumber(Number number, string calculationId)
        {
            if (number.Index >= 50)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Calculation #{calculationId}: reached MAX number count. Increase count in app settings if needed.");
                Console.WriteLine($"Calculation #{calculationId}: last received number {number}. Check logs for the full output.");
                Console.WriteLine();
                Console.ResetColor();

                return;
            }

            Console.WriteLine($"Received: {number}");

            var next = CalcService.GetNextNumber(number);

            await SendNumber(next, calculationId);
        }

        private static async Task SendNumber(Number number, string calculationId)
        {
            string message = $"Sending: {number}";
            Console.WriteLine(message);

            var logger = LogService.CreateLogger(calculationId);

            logger.Info(message);

            await HttpService.SendAsync(number, calculationId);
        }
    }
}
