using System;

namespace LoggerLibrary
{
    public interface ILogger
    {
        void LogInfo(string tag, string message);
        void LogDebug(string tag, string message, object data);
        void LogWarning(string tag, string message);
        void LogError(string tag, string message, Exception ex);
        void PushLogsToFile(string filePath = null, bool overwrite = true);
    }
}