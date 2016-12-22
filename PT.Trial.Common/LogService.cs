using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace PT.Trial.Common
{
    public class LogService : ILogService
    {
        public static string LogsFolder { get; set; } = "Logs";

        static LogService()
        {
            var config = new LoggingConfiguration();

            AddFileTarget("", config);
            AddConsoleTarget("", config);

            LogManager.Configuration = config;
        }

        public ILogger CreateLogger(string calculationId)
        {
            var logger = LogManager.GetLogger(calculationId);
            logger.Trace("Logger is initialized");

            return logger;
        }

        private static void AddFileTarget(string calculationId, LoggingConfiguration config)
        {
            string start = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var target = new FileTarget("file");
            target.FileName = LogsFolder + $"/{start} - Calc #${{logger}}.log";
            target.Layout = "${date:format=HH\\:MM\\:ss} #${logger} ${message}";

            config.AddTarget(target);

            var rule = new LoggingRule("*", LogLevel.Trace, target);
            config.LoggingRules.Add(rule);
        }

        private static void AddConsoleTarget(string calculationId, LoggingConfiguration config)
        {
            var target = new ColoredConsoleTarget("cc");
            target.Layout = "${message}";

            config.AddTarget(target);

            var rule = new LoggingRule("*", LogLevel.Info, target);
            config.LoggingRules.Add(rule);
        }
    }
}