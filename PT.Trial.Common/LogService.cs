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
            string start = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var target = new FileTarget();
            target.Name = calculationId;
            target.FileName = LogsFolder + $"/{start} - Thread #{calculationId}.log";
            target.Layout = "${date:format=HH\\:MM\\:ss} ${logger} ${message}";

            var config = new LoggingConfiguration();
            config.AddTarget(calculationId, target);

            var rule = new LoggingRule("*", LogLevel.Info, target);
            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;

            var _logger = LogManager.GetLogger(calculationId);
            _logger.Info("Logger is initialized");

            return _logger;
        }
    }
}