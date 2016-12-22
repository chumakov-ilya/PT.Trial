using NLog;

namespace PT.Trial.Common.Contracts
{
    public interface ILogService {
        ILogger CreateLogger(string calculationId);
    }
}