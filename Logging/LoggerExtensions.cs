using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace pawd.CoreLibrary.Logging
{
    public static class LoggerExtensions
    {
        public static void LogWithCallerInfo(
            this Serilog.ILogger logger,
            string messageTemplate,
            LogLevel level,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "",
            params object[] propertyValues)
        {
            LogWithCallerInfo(logger, messageTemplate, level, propertyValues, filePath, lineNumber, memberName);
        }
        
        public static void LogWithCallerInfo(
            this Serilog.ILogger logger,
            string messageTemplate,
            LogLevel level = LogLevel.Information,
            object[]? propertyValues = null,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "")
        {
            var prefix = $"[{Path.GetFileName(filePath)}:{lineNumber} - {memberName}] ";
            var fullMessageTemplate = prefix + messageTemplate;

            propertyValues ??= [];

            switch (level)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                    logger.Debug(fullMessageTemplate, propertyValues);
                    break;
                case LogLevel.Information:
                    logger.Information(fullMessageTemplate, propertyValues);
                    break;
                case LogLevel.Warning:
                    logger.Warning(fullMessageTemplate, propertyValues);
                    break;
                case LogLevel.Error:
                    logger.Error(fullMessageTemplate, propertyValues);
                    break;
                case LogLevel.Critical:
                    logger.Fatal(fullMessageTemplate, propertyValues);
                    break;
                case LogLevel.None:
                default:
                    logger.Information(fullMessageTemplate, propertyValues);
                    break;
            }
        }
    }
}
