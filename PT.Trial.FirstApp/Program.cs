using System;
using System.Threading.Tasks;
using EasyNetQ;
using PT.Trial.Common;

namespace PT.Trial.FirstApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = ParseTaskCount(args);

            Console.WriteLine($"Task count: {count}");

            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                string threadId = "1";

                bus.Subscribe<Number>("test", async x => await HandleNumber(x, threadId), x => x.WithTopic(threadId));

                SendNumber(new Number(1, 1), threadId).Wait();

                Console.WriteLine("Listening for messages. Hit <return> to quit.");
                Console.ReadLine();
            }
        }

        private static int ParseTaskCount(string[] args)
        {
            const int defaultCount = 1;
            if (args == null || args.Length == 0) return defaultCount;

            int taskCount;
            bool isParsed = int.TryParse(args[1], out taskCount);

            if (!isParsed) return defaultCount;

            return taskCount;
        }

        static async Task HandleNumber(Number number, string threadId)
        {
            if (number.Index >= 50) return;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Received: {number}");
            Console.ResetColor();

            var next = CalcService.GetNextNumber(number);

            await SendNumber(next, threadId);
        }

        private static async Task SendNumber(Number number, string threadId)
        {
            Console.WriteLine($"Sending: {number}");

            await HttpService.SendAsync(number, threadId);
        }
    }
}
