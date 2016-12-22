using System;
using NLog;
using NLog.Config;
using NLog.Targets;
using PT.Trial.Common.Contracts;

namespace PT.Trial.Common.Services
{
    public class LogService : ILogService
    {
        public static string LogsFolder { get; } = "Logs";

        static LogService()
        {
            var config = new LoggingConfiguration();

            AddFileTarget(config);
            AddConsoleTarget(config);

            LogManager.Configuration = config;
        }

        public ILogger CreateLogger(string calculationId)
        {
            var logger = LogManager.GetLogger(calculationId);
            logger.Trace("Logger is initialized");

            return logger;
        }

        private static void AddFileTarget(LoggingConfiguration config)
        {
            string start = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var target = new FileTarget("file");
            target.FileName = LogsFolder + $"/{start} - Calc #${{logger}}.log";
            target.Layout = "${date:format=HH\\:MM\\:ss} #${logger} ${message}";

            config.AddTarget(target);

            var rule = new LoggingRule("*", LogLevel.Trace, target);
            config.LoggingRules.Add(rule);
        }

        private static void AddConsoleTarget(LoggingConfiguration config)
        {
            var target = new ColoredConsoleTarget("cc");
            target.Layout = "${message}";

            config.AddTarget(target);

            var rule = new LoggingRule("*", LogLevel.Info, target);
            config.LoggingRules.Add(rule);
        }
    }
}