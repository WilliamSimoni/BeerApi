namespace Domain.Logger
{
    public interface ILoggerManager
    {
        void LogDebug(string message);
        void LogDebug<T0>(string message, T0 param1);
        void LogDebug<T0, T1>(string message, T0 param1, T1 param2);
        void LogDebug<T0, T1, T2>(string message, T0 param1, T1 param2, T2 param3);
        void LogInfo(string message);
        void LogInfo<T0>(string message, T0 param1);
        void LogInfo<T0, T1>(string message, T0 param1, T1 param2);
        void LogInfo<T0, T1, T2>(string message, T0 param1, T1 param2, T2 param3);
        void LogWarn(string message);
        void LogWarn<T0>(string message, T0 param1);
        void LogWarn<T0, T1>(string message, T0 param1, T1 param2);
        void LogWarn<T0, T1, T2>(string message, T0 param1, T1 param2, T2 param3);
        void LogError(string message);
        void LogError<T0>(string message, T0 param1);
        void LogError<T0, T1>(string message, T0 param1, T1 param2);
        void LogError<T0, T1, T2>(string message, T0 param1, T1 param2, T2 param3);

    }
}
