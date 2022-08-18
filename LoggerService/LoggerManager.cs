using Domain.Logger;
using Microsoft.Extensions.Logging;
using Serilog;

namespace LoggerService
{
    public class LoggerManager : ILoggerManager
    {
        public void LogDebug(string message)
        {
            Log.Logger.Debug(message);
        }

        public void LogDebug<T0>(string message, T0 param1)
        {
            Log.Logger.Debug(message, param1);
        }

        public void LogDebug<T0, T1>(string message, T0 param1, T1 param2)
        {
            Log.Logger.Debug(message, param1, param2);
        }

        public void LogDebug<T0, T1, T2>(string message, T0 param1, T1 param2, T2 param3)
        {
            Log.Logger.Debug(message, param1, param2, param3);
        }

        public void LogInfo(string message)
        {
            Log.Logger.Information(message);
        }

        public void LogInfo<T0>(string message, T0 param1)
        {
            Log.Logger.Information(message, param1);
        }

        public void LogInfo<T0, T1>(string message, T0 param1, T1 param2)
        {
            Log.Logger.Information(message, param1, param2);
        }

        public void LogInfo<T0, T1, T2>(string message, T0 param1, T1 param2, T2 param3)
        {
            Log.Logger.Information(message, param1, param2, param3);
        }

        public void LogWarn(string message)
        {
            Log.Logger.Warning(message);
        }

        public void LogWarn<T0>(string message, T0 param1)
        {
            Log.Logger.Warning(message);
        }

        public void LogWarn<T0, T1>(string message, T0 param1, T1 param2)
        {
            Log.Logger.Warning(message, param1, param2);
        }

        public void LogWarn<T0, T1, T2>(string message, T0 param1, T1 param2, T2 param3)
        {
            Log.Logger.Warning(message, param1, param2, param3);
        }

        public void LogError(string message)
        {
            Log.Logger.Error(message);
        }

        public void LogError<T0>(string message, T0 param1)
        {
            Log.Logger.Error(message, param1);
        }

        public void LogError<T0, T1>(string message, T0 param1, T1 param2)
        {
            Log.Logger.Error(message, param1, param2);
        }

        public void LogError<T0, T1, T2>(string message, T0 param1, T1 param2, T2 param3)
        {
            Log.Logger.Error(message, param1, param2, param3);
        }
    }
}