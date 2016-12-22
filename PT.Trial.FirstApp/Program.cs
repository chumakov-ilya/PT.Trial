using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using PT.Trial.Common;

namespace PT.Trial.FirstApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Root.CreateContainer();

            var busService = container.Resolve<IBusService>();

            var bus = busService.CreateBus();

            try
            {
                int count = ParseTaskCount(args);

                var tasks = new Task[count];

                for (int index = 0; index < count; index++)
                {
                    var calculation = container.Resolve<Calculation>(new NamedParameter("id", index.ToString()));

                    var task = Task.Factory.StartNew(() =>
                    {
                        calculation.SubscribeTo(bus);

                        calculation.SendStartNumber();
                    });

                    tasks[index] = task;
                }

                Task.WaitAll(tasks);

                Console.WriteLine($"{count} parallel calculations are runned. Hit <Enter> to quit.");

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

    }
}
