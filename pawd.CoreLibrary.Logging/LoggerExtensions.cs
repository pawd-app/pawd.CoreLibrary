using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace pawd.CoreLibrary.Logging
{

    public static class LoggerExtensions
    {
        public static void LogWithCallerInfo(
            this Serilog.ILogger logger,
            string message,
            LogLevel level = LogLevel.Information,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "")
        {
            var logMessage = $"[{Path.GetFileName(filePath)}:{lineNumber} - {memberName}] {message}";

            switch (level)
            {
                case LogLevel.Debug:
                    logger.Debug(logMessage);
                    break;
                case LogLevel.Information:
                    logger.Information(logMessage);
                    break;
                case LogLevel.Warning:
                    logger.Warning(logMessage);
                    break;
                case LogLevel.Error:
                    logger.Error(logMessage);
                    break;
                case LogLevel.Critical:
                    logger.Fatal(logMessage);
                    break;
                default:
                    logger.Information(logMessage);
                    break;
            }
        }
    }
}
