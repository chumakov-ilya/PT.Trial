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
            int count = ParseCalculationCount();

            var container = Root.CreateContainer();

            var busService = container.Resolve<IBusService>();

            var bus = busService.CreateBus();

            try
            {
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

        private static int ParseCalculationCount()
        {
            Console.Write("Enter count of parallel calculations = ");
            string input = Console.ReadLine();

            int count;
            bool isParsed = int.TryParse(input, out count);

            if (!isParsed) throw new ArgumentException("wrong input");

            return count;
        }

    }
}
