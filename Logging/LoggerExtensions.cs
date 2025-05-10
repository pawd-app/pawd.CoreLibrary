using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace pawd.CoreLibrary.Logging
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using Serilog;

    public static class LoggerExtensions
    {
        private static string FormatMessageTemplate(
            string messageTemplate,
            string filePath,
            int lineNumber,
            string memberName)
        {
            var prefix = $"[{Path.GetFileName(filePath)}:{lineNumber} - {memberName}] ";
            return prefix + messageTemplate;
        }

        public static void LogErrorWithCallerInfo(this ILogger logger,
            string messageTemplate,
            Exception? exception = null,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "",
            params object[] args)
        {
            var msg = FormatMessageTemplate(messageTemplate, filePath, lineNumber, memberName);
            if (exception != null)
                logger.Error(exception, msg, args);
            else
                logger.Error(msg, args);
        }

        public static void LogWarningWithCallerInfo(this ILogger logger,
            string messageTemplate,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "",
            params object[] args)
        {
            var msg = FormatMessageTemplate(messageTemplate, filePath, lineNumber, memberName);
            logger.Warning(msg, args);
        }

        public static void LogInformationWithCallerInfo(this ILogger logger,
            string messageTemplate,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "",
            params object[] args)
        {
            var msg = FormatMessageTemplate(messageTemplate, filePath, lineNumber, memberName);
            logger.Information(msg, args);
        }

        public static void LogDebugWithCallerInfo(this ILogger logger,
            string messageTemplate,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "",
            params object[] args)
        {
            var msg = FormatMessageTemplate(messageTemplate, filePath, lineNumber, memberName);
            logger.Debug(msg, args);
        }

        public static void LogCriticalWithCallerInfo(this ILogger logger,
            string messageTemplate,
            Exception? exception = null,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "",
            params object[] args)
        {
            var msg = FormatMessageTemplate(messageTemplate, filePath, lineNumber, memberName);
            if (exception != null)
                logger.Fatal(exception, msg, args);
            else
                logger.Fatal(msg, args);
        }

        public static void LogTraceWithCallerInfo(this ILogger logger,
            string messageTemplate,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "",
            params object[] args)
        {
            var msg = FormatMessageTemplate(messageTemplate, filePath, lineNumber, memberName);
            logger.Verbose(msg, args);
        }
    }
}