using NLog;

namespace PT.Trial.Common
{
    public interface ILogService {
        ILogger CreateLogger(string calculationId);
    }
}