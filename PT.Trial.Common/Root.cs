using Autofac;
using PT.Trial.Common.Contracts;
using PT.Trial.Common.Services;

namespace PT.Trial.Common
{
    public class Root
    {
        public static IContainer CreateContainer()
        {
            var builder = CreateBuilder();

            return builder.Build();
        }

        public static ContainerBuilder CreateBuilder()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<BusService>().As<IBusService>().InstancePerDependency();
            builder.RegisterType<CalcService>().As<ICalcService>().InstancePerDependency();
            builder.RegisterType<HttpService>().As<IHttpService>().InstancePerDependency();
            builder.RegisterType<LogService>().As<ILogService>().InstancePerDependency();

            builder.RegisterType<Calculation>();
            return builder;
        }
    }
}