using Invoicing.Base.Helpers;
using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Invoicing.Base.Logging.Factory
{
    internal static class MessageFactory
    {
        // Keeps track of the amount of unnamed threads.
        private static int _threads;

        public static string CreateMessage(bool extended, Exception exception, DateTime logDateTime, string message, LogLevel level, string loggerName, int lineNumber, string className)
        {
            if (extended)
                return CreateExtendedMessage(exception, logDateTime, message, level, loggerName, lineNumber, className);

            return CreateNormalMessage(exception, logDateTime, message, level, lineNumber);
        }

        public static string CreateObjectMessage<T>(string message, T oldObj, T newObj, DateTime logDateTime, LogLevel level, string loggerName, int lineNumber, string className)
        {
            return CheckObjectDifferences(oldObj, newObj, message, logDateTime, level, lineNumber);
        }

        private static string CheckObjectDifferences<T>(T oldObj, T newObj, string message, DateTime logDateTime, LogLevel level, int lineNumber)
        {
            var builder = new StringBuilder();
            var loop = 0;

            if (!string.IsNullOrEmpty(message))
                message = message.TrimEnd() + " ";

            if (oldObj == null || newObj == null)
            {
                object obj;

                if (oldObj == null)
                {
                    builder.Append("Creating object => ");
                    obj = newObj;
                }
                else
                {
                    builder.Append("Deleting object => ");
                    obj = oldObj;
                }

                FieldInfo[] info = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                foreach (var field in info)
                {
                    if (!typeof(IEnumerable).IsAssignableFrom(field.FieldType) || field.FieldType == typeof(string))
                    {
                        if (field.FieldType.IsPrimitive || field.FieldType == typeof(decimal) || field.FieldType == typeof(string))
                        {
                            if (loop > 0)
                                builder.Append(" ; ");
                            var name = field.Name
                                    .Replace(">", "")
                                    .Replace("<", "")
                                    .Replace("kBackingField", "")
                                    .Replace("_","");

                            builder.Append($"{name} = {field.GetValue(obj)}");
                            loop++;
                        }
                    }
                }
            }
            else
            {
                if (oldObj.GetType() == newObj.GetType())
                {
                    var differences = ObjectHelper.DetailedCompare(oldObj, newObj);
                    if (differences.Count > 0)
                    {
                        builder.Append("Saving changes => ");
                        foreach (var diff in differences)
                        {
                            if (loop > 0)
                                builder.Append(" ; ");
                            builder.Append($"{diff.Property.Replace("_", "")}: [Old] = {diff.OldVal} , [New] = {diff.NewVal}");
                            loop++;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return CreateNormalMessage(null, logDateTime, message + builder.ToString(), level, lineNumber);
        }

        public static string CreateNormalMessage(Exception exception, DateTime logDateTime, string message, LogLevel level, int lineNumber)
        {
            //Declaration
            StringBuilder logMessage = new StringBuilder();

            //Sets the datetime string (formatted according to culture)
            var dateTimeNow = ConvertDateTimeToString(logDateTime);
            //Converts the logLevel to a string.
            var logLevel = GetLogLevel(level);

            var threadName = CheckThreadName();
            threadName = "[" + threadName + "]";

            if (exception == null)
            {

                if (lineNumber == -1)
                    logMessage.AppendFormat(CultureInfo.CurrentCulture, "{0} {1} {2} {3} {4}", dateTimeNow,
                        logLevel.ToUpper().PadRight(7), threadName.PadRight(13), message);
                else
                    logMessage.AppendFormat(CultureInfo.CurrentCulture, "{0} {1} Line: {2} {3} {4}", dateTimeNow,
                        logLevel.ToUpper().PadRight(7), lineNumber.ToString().PadRight(4), threadName.PadRight(13), message);

            }
            else
            {

                if (exception.StackTrace != null && exception.LineNumber() != -1)
                    lineNumber = exception.LineNumber();

                if (exception.StackTrace != null)
                {
                    if (lineNumber == -1)
                        logMessage.AppendFormat(CultureInfo.CurrentCulture, "{0} {1} {2} {3} \n {4}", dateTimeNow,
                            logLevel.ToUpper().PadRight(7), threadName.PadRight(13), message, exception);
                    else
                        logMessage.AppendFormat(CultureInfo.CurrentCulture, "{0} {1} Line: {2} {3} {4} \n {5}", dateTimeNow,
                            logLevel.ToUpper().PadRight(7), lineNumber.ToString().PadRight(4), threadName.PadRight(13), message, exception);
                }
                else
                {
                    if (lineNumber == -1)
                        logMessage.AppendFormat(CultureInfo.CurrentCulture, "{0} {1} {2} - {3} -> {4} - {5}", dateTimeNow,
                            logLevel.ToUpper().PadRight(7), threadName.PadRight(13), message, exception);
                    else
                        logMessage.AppendFormat(CultureInfo.CurrentCulture, "{0} {1} Line: {2} {3} {4} -> {5}", dateTimeNow,
                            logLevel.ToUpper().PadRight(7), lineNumber.ToString().PadRight(4), threadName.PadRight(13), message, exception);
                }

            }

            return logMessage.ToString();
        }

        public static string CreateExtendedMessage(Exception exception, DateTime logDateTime, string message, LogLevel level, string loggerName, int lineNumber, string classname)
        {

            //Declaration
            //string logMessage;
            StringBuilder logMessage;
            //Sets the datetime string (formatted according to culture)
            var dateTimeNow = ConvertDateTimeToString(logDateTime);
            //Converts the logLevel to a string.
            var logLevel = GetLogLevel(level);


            if (exception == null)
            {
                logMessage = ExtendedLogMessage(logLevel, dateTimeNow, loggerName, message);
                if (lineNumber != -1)
                    logMessage.AppendLine(" = Line number          : " + lineNumber);
                if (classname != null)
                    logMessage.AppendLine(" = Origin Class name    : " + classname);
                logMessage.AppendLine("=======================================================================================");

            }
            else
            {

                if (exception.StackTrace != null && exception.LineNumber() != -1)
                    lineNumber = exception.LineNumber();

                if (exception.StackTrace == null)
                {
                    logMessage = ExtendedLogMessage(logLevel, dateTimeNow, loggerName, message);
                    if (classname != null)
                        logMessage.AppendLine(" = Origin Class name    : " + classname);
                    if (exception.TargetSite != null)
                        logMessage.AppendLine(" = Class                : " + exception.TargetSite.ReflectedType?.Name);
                    if (exception.Message != null)
                        logMessage.AppendLine(" = Exception Message    : " + exception.Message);
                    if (lineNumber != -1)
                        logMessage.AppendLine(" = Line number          : " + lineNumber);
                    logMessage.AppendLine("=======================================================================================");

                }
                else
                {
                    logMessage = ExtendedLogMessage(logLevel, dateTimeNow, loggerName, message);
                    if (lineNumber != -1)
                        logMessage.AppendLine(" = Line Number          : " + lineNumber);
                    if (classname != null)
                        logMessage.AppendLine(" = Origin Class name    : " + classname);
                    logMessage.AppendLine(" = Method               : " + exception.TargetSite);
                    if (exception.TargetSite != null)
                        logMessage.AppendLine(" = Class                : " + exception.TargetSite.ReflectedType?.Name);
                    logMessage.AppendLine(" = Exception Type       : " + exception.GetType());
                    logMessage.AppendLine(" = Exception Message    : " + exception.Message);
                    logMessage.AppendLine(" = Exception Source     : " + exception.Source);
                    logMessage.AppendLine(" = Exception StackTrace : " + Environment.NewLine + exception.StackTrace);
                    logMessage.AppendLine(" = Exception Data       : " + exception.Data);
                    logMessage.AppendLine(" = Inner Exception      : " + exception.InnerException);
                    logMessage.AppendLine("=======================================================================================");
                }
            }



            return logMessage.ToString();

        }

        public static StringBuilder ExtendedLogMessage(string logLevel, string dateTimeNow, string loggerName, string message)
        {
            StringBuilder logMessage = new StringBuilder();

            var threadName = CheckThreadName();

            logMessage.AppendLine("=======================================================================================");
            logMessage.AppendLine("||" + logLevel.ToUpper().PadRight(10) + " : " + dateTimeNow);
            logMessage.AppendLine("=======================================================================================");
            logMessage.AppendLine(" = Logger Name          : " + loggerName);
            logMessage.AppendLine(" = Message              : " + message);
            logMessage.AppendLine(" = Thread Name          : " + threadName);

            return logMessage;
        }

        //Converts DateTime to a string according to cultureInfo. (uses CurrentCulture.)
        internal static string ConvertDateTimeToString(DateTime time)
        {
            var cultureInfo = CultureInfo.CurrentCulture;
            //CultureInfo us = new CultureInfo("en-US");
            var shortDateFormatString = cultureInfo.DateTimeFormat.ShortDatePattern;
            var longTimeFormatString = cultureInfo.DateTimeFormat.LongTimePattern;

            return time.ToString(shortDateFormatString + " " + longTimeFormatString, cultureInfo);

        }

        private static string CheckThreadName()
        {
            var threadName = GetCurrentThreadName();
            if (threadName == null)
            {
                if (CheckForMainThreadConsole() || CheckForMainThreadGui())
                {
                    threadName = "MainThread";
                    Thread.CurrentThread.Name = "MainThread";
                }
                else
                {
                    _threads++;
                    threadName = "Thread " + _threads;
                    Thread.CurrentThread.Name = threadName;
                }
            }

            return threadName;
        }

        //Gets the name of the current thread
        private static string GetCurrentThreadName()
        {
            return Thread.CurrentThread.Name;
        }

        //Convert logLevel to string.
        private static string GetLogLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return "Debug";
                case LogLevel.Error:
                    return "Error";
                case LogLevel.Fatal:
                    return "Fatal";
                case LogLevel.Information:
                    return "Info";
                case LogLevel.Trace:
                    return "Trace";
                case LogLevel.Warning:
                    return "Warning";
                case LogLevel.CrudInfo:
                    return "DB Info";
                case LogLevel.Off:
                    throw new LoggerException("Trying to log when loglevel is set to OFF...");
                default:
                    throw new LoggerException("This should not happen...");
            }
        }

        //Checks if a thread is the main thread for a console app.
        private static bool CheckForMainThreadConsole()
        {
            if (Thread.CurrentThread.GetApartmentState() == ApartmentState.MTA &&
                !Thread.CurrentThread.IsBackground && !Thread.CurrentThread.IsThreadPoolThread && Thread.CurrentThread.IsAlive)
            {
                MethodInfo correctEntryMethod = Assembly.GetEntryAssembly()?.EntryPoint;
                StackTrace trace = new StackTrace();
                StackFrame[] frames = trace.GetFrames();
                if (frames == null) return false;
                for (int i = frames.Length - 1; i >= 0; i--)
                {
                    MethodBase method = frames[i].GetMethod();
                    if (correctEntryMethod == method)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        //Checks if a thread is the main thread for a UI app.
        private static bool CheckForMainThreadGui()
        {
            if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA &&
                !Thread.CurrentThread.IsBackground && !Thread.CurrentThread.IsThreadPoolThread && Thread.CurrentThread.IsAlive)
            {
                MethodInfo correctEntryMethod = Assembly.GetEntryAssembly()?.EntryPoint;
                StackTrace trace = new StackTrace();
                StackFrame[] frames = trace.GetFrames();
                if (frames == null) return false;
                for (int i = frames.Length - 1; i >= 0; i--)
                {
                    MethodBase method = frames[i].GetMethod();
                    if (correctEntryMethod == method)
                    {
                        return true;
                    }
                }
            }

            return false;
        }



    }
}
