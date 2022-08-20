using Domain.Logger;
using Microsoft.Extensions.Logging;
using Serilog;

namespace LoggerService
{
    public class LoggerManager : ILoggerManager
    {

        private readonly ILogger<LoggerManager> _logger;
        public LoggerManager(ILogger<LoggerManager> logger)
        {
            _logger = logger;
        }
        public void LogDebug(string message)
        {
            _logger.LogDebug(message);
        }

        public void LogDebug<T0>(string message, T0 param1)
        {
            _logger.LogDebug(message, param1);
        }

        public void LogDebug<T0, T1>(string message, T0 param1, T1 param2)
        {
            _logger.LogDebug(message, param1, param2);
        }

        public void LogDebug<T0, T1, T2>(string message, T0 param1, T1 param2, T2 param3)
        {
            _logger.LogDebug(message, param1, param2, param3);
        }

        public void LogInfo(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogInfo<T0>(string message, T0 param1)
        {
            _logger.LogInformation(message, param1);
        }

        public void LogInfo<T0, T1>(string message, T0 param1, T1 param2)
        {
            _logger.LogInformation(message, param1, param2);
        }

        public void LogInfo<T0, T1, T2>(string message, T0 param1, T1 param2, T2 param3)
        {
            _logger.LogInformation(message, param1, param2, param3);
        }

        public void LogWarn(string message)
        {
            _logger.LogWarning(message);
        }

        public void LogWarn<T0>(string message, T0 param1)
        {
            _logger.LogWarning(message);
        }

        public void LogWarn<T0, T1>(string message, T0 param1, T1 param2)
        {
            _logger.LogWarning(message, param1, param2);
        }

        public void LogWarn<T0, T1, T2>(string message, T0 param1, T1 param2, T2 param3)
        {
            _logger.LogWarning(message, param1, param2, param3);
        }

        public void LogError(string message)
        {
            _logger.LogError(message);
        }

        public void LogError<T0>(string message, T0 param1)
        {
            _logger.LogError(message, param1);
        }

        public void LogError<T0, T1>(string message, T0 param1, T1 param2)
        {
            _logger.LogError(message, param1, param2);
        }

        public void LogError<T0, T1, T2>(string message, T0 param1, T1 param2, T2 param3)
        {
            _logger.LogError(message, param1, param2, param3);
        }
    }
}