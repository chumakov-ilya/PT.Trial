using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace PT.Trial.Common
{
    public class LogService
    {
        public static string LogsFolder { get; set; } = "Logs";

        public static ILogger CreateLogger(string calculationId)
        {
            var config = new LoggingConfiguration();

            AddFileTarget(calculationId, config);
            AddConsoleTarget(calculationId, config);

            LogManager.Configuration = config;

            var _logger = LogManager.GetLogger(calculationId);
            _logger.Trace("Logger is initialized");

            return _logger;
        }

        private static void AddFileTarget(string calculationId, LoggingConfiguration config)
        {
            string start = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var target = new FileTarget();
            target.Name = calculationId;
            target.FileName = LogsFolder + $"/{start} - Thread #{calculationId}.log";
            target.Layout = "${date:format=HH\\:MM\\:ss} ${logger} ${message}";

            config.AddTarget(calculationId, target);

            var rule = new LoggingRule("*", LogLevel.Trace, target);
            config.LoggingRules.Add(rule);
        }

        private static void AddConsoleTarget(string calculationId, LoggingConfiguration config)
        {
            var target = new ColoredConsoleTarget(calculationId);
            target.Layout = "${message}";

            config.AddTarget(calculationId, target);

            var rule = new LoggingRule("*", LogLevel.Info, target);
            config.LoggingRules.Add(rule);
        }
    }
}