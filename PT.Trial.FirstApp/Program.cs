using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using PT.Trial.Common;
using PT.Trial.Common.Contracts;

namespace PT.Trial.FirstApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Root.CreateContainer();

            var busService = container.Resolve<IBusService>();
            var settings = container.Resolve<AppSettings>();

            var bus = busService.CreateBus();

            try
            {
                int count = ParseCalculationCount(args, settings);

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

        private static int ParseCalculationCount(string[] args, AppSettings settings)
        {
            int defaultCount = settings.DefaultCalculationsCount;

            if (args == null || args.Length == 0) return defaultCount;

            int taskCount;
            bool isParsed = int.TryParse(args[1], out taskCount);

            if (!isParsed) return defaultCount;

            return taskCount;
        }

    }
}
