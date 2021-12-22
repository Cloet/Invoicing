using Microsoft.Extensions.Logging;
using System;

namespace Invoicing.Base.Logging
{
    public interface ICLogger : ILogger
    {
        string Name { get; }

        string ShortName { get; }

        /// <summary>
        /// The namespace
        /// </summary>
        string NameSpace { get; }

        /// <summary>
        /// The classname
        /// </summary>
        string ClassName { get; }

        /// <summary>
        /// Separate each loglevel to different files
        /// </summary>
        bool SeparateLogLevelFiles { get; }

        /// <summary>
        /// The current loglevel
        /// </summary>
        LogLevel LoggerLevel { get; }

        /// <summary>
        /// Returns the location the logs will be stored.
        /// </summary>
        string Location { get; }

        /// <summary>
        /// Traces all changes between two objects and writes ta logfile.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldObj"></param>
        /// <param name="newObj"></param>
        void TraceObjectSave<T>(T oldObj, T newObj);

        /// <summary>
        /// Traces all changes between two objects and gives the ability to add an additional message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="oldObj"></param>
        /// <param name="newObj"></param>
        void TraceObjectSave<T>(string message, T oldObj, T newObj);

        /// <summary>
        /// Traces the object creation and the initial properties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="createdObject"></param>
        void TraceObjectCreation<T>(T createdObject);

        /// <summary>
        /// Traces the object creation and the initial properties with an additional message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="createdObject"></param>
        void TraceObjectCreation<T>(string message, T createdObject);

        /// <summary>
        /// Traces object deletion and the values the object had before deletion.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="deletedObject"></param>
        void TraceObjectDeletion<T>(T deletedObject);

        /// <summary>
        /// Traces object deletion and the values the object had before deletion with an additional message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="deletedObject"></param>
        void TraceObjectDeletion<T>(string message, T deletedObject);

        /// <summary>
        /// Logs a Trace message
        /// </summary>
        /// <param name="exception"></param>
        void Trace(Exception exception);

        /// <summary>
        /// Logs a Trace message
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        void Trace(Exception exception, string message);

        /// <summary>
        /// Logs a Trace Message
        /// </summary>
        /// <param name="message"></param>
        void Trace(string message);

        /// <summary>
        /// Logs a Trace Message
        /// </summary>
        /// <param name="format">The string format to log.</param>
        /// <param name="args">The arguments in the string format.</param>
        void TraceFormat(string format, params object[] args);

        /// <summary>
        /// Logs a Trace Message
        /// </summary>
        /// <param name="exception">The Exception that has been thrown.</param>
        /// <param name="format">The string format.</param>
        /// <param name="args">The arguments used in the string format.</param>
        void TraceFormat(Exception exception, string format, params object[] args);

        /// <summary>
        /// Logs a Trace Message
        /// </summary>
        /// <param name="provider">The string format, formatProvider.</param>
        /// <param name="format">The format of the string.</param>
        /// <param name="args">The format arguments.</param>
        void TraceFormat(IFormatProvider provider, string format, params object[] args);

        /// <summary>
        /// Logs a Trace Message
        /// </summary>
        /// <param name="exception">The Exception that is thrown.</param>
        /// <param name="provider">The provider used in the string format.</param>
        /// <param name="format">The string format.</param>
        /// <param name="args">The arguments.</param>
        void TraceFormat(Exception exception, IFormatProvider provider, string format, params object[] args);

        /// <summary>
        /// Logs an Error Message
        /// </summary>
        /// <param name="exception"></param>
        void Error(Exception exception);

        /// <summary>
        /// Logs an Error Message
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        void Error(Exception exception, string message);

        /// <summary>
        /// Logs an Error Message
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);

        /// <summary>
        /// Logs an Error Message
        /// </summary>
        /// <param name="format">The string format to log.</param>
        /// <param name="args">The arguments in the string format.</param>
        void ErrorFormat(string format, params object[] args);

        /// <summary>
        /// Logs an Error Message
        /// </summary>
        /// <param name="exception">The Exception that has been thrown.</param>
        /// <param name="format">The string format.</param>
        /// <param name="args">The string format arguments.</param>
        void ErrorFormat(Exception exception, string format, params object[] args);

        /// <summary>
        /// Logs an Error Message
        /// </summary>
        /// <param name="provider">The string format, formatProvider.</param>
        /// <param name="format">The format of the string.</param>
        /// <param name="args">The format arguments.</param>
        void ErrorFormat(IFormatProvider provider, string format, params object[] args);

        /// <summary>
        /// Logs an Error Message
        /// </summary>
        /// <param name="exception">The Exception that is thrown.</param>
        /// <param name="provider">The provider used in the string format.</param>
        /// <param name="format">The string format.</param>
        /// <param name="args">The arguments.</param>
        void ErrorFormat(Exception exception, IFormatProvider provider, string format, params object[] args);

        /// <summary>
        /// Logs an Information Message
        /// </summary>
        /// <param name="exception"></param>
        void Info(Exception exception);

        /// <summary>
        /// Logs an Information Message
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        void Info(Exception exception, string message);

        /// <summary>
        /// Logs an Information Message
        /// </summary>
        /// <param name="message"></param>
        void Info(string message);

        /// <summary>
        /// Logs an Information Message
        /// </summary>
        /// <param name="format">The string format to log.</param>
        /// <param name="args">The format string arguments.</param>
        void InfoFormat(string format, params object[] args);

        /// <summary>
        /// Logs an Information Message
        /// </summary>
        /// <param name="exception">Exception that has been thrown.</param>
        /// <param name="format">The string format.</param>
        /// <param name="args">The arguments for the format.</param>
        void InfoFormat(Exception exception, string format, params object[] args);

        /// <summary>
        /// Logs an Info Message
        /// </summary>
        /// <param name="provider">The string format, formatProvider.</param>
        /// <param name="format">The format of the string.</param>
        /// <param name="args">The format arguments.</param>
        void InfoFormat(IFormatProvider provider, string format, params object[] args);

        /// <summary>
        /// Logs an Info Message
        /// </summary>
        /// <param name="exception">The Exception that is thrown.</param>
        /// <param name="provider">The provider used in the string format.</param>
        /// <param name="format">The string format.</param>
        /// <param name="args">The arguments.</param>
        void InfoFormat(Exception exception, IFormatProvider provider, string format, params object[] args);

        /// <summary>
        /// Logs a Debug Message.
        /// </summary>
        /// <param name="exception"></param>
        void Debug(Exception exception);

        /// <summary>
        /// Logs a Debug Message
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        void Debug(Exception exception, string message);

        /// <summary>
        /// Logs a Debug Message
        /// </summary>
        /// <param name="message"></param>
        void Debug(string message);

        /// <summary>
        /// Logs a Debug Message
        /// </summary>
        /// <param name="format">The string format.</param>
        /// <param name="args">The string format arguments.</param>
        void DebugFormat(string format, params object[] args);

        /// <summary>
        /// Logs a Debug Message
        /// </summary>
        /// <param name="exception">The Exception that has been thrown.</param>
        /// <param name="format">The string format.</param>
        /// <param name="args">The arguments for the string format.</param>
        void DebugFormat(Exception exception, string format, params object[] args);

        /// <summary>
        /// Logs a Debug Message
        /// </summary>
        /// <param name="provider">The string format, formatProvider.</param>
        /// <param name="format">The format of the string.</param>
        /// <param name="args">The format arguments.</param>
        void DebugFormat(IFormatProvider provider, string format, params object[] args);

        /// <summary>
        /// Logs a Debug Message
        /// </summary>
        /// <param name="exception">The Exception that is thrown.</param>
        /// <param name="provider">The provider used in the string format.</param>
        /// <param name="format">The string format.</param>
        /// <param name="args">The arguments.</param>
        void DebugFormat(Exception exception, IFormatProvider provider, string format, params object[] args);

        /// <summary>
        /// Logs a Warning Message
        /// </summary>
        /// <param name="exception"></param>
        void Warning(Exception exception);

        /// <summary>
        /// Logs a Warning message
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        void Warning(Exception exception, string message);

        /// <summary>
        /// Logs a Warning message
        /// </summary>
        /// <param name="message"></param>
        void Warning(string message);

        /// <summary>
        /// Logs a Warning Message.
        /// </summary>
        /// <param name="format">The string format.</param>
        /// <param name="args">The string format arguments.</param>
        void WarningFormat(string format, params object[] args);

        /// <summary>
        /// Logs a Warning Message.
        /// </summary>
        /// <param name="exception">The Exception that has been thrown.</param>
        /// <param name="format">The Format string.</param>
        /// <param name="args">The arguments for the format.</param>
        void WarningFormat(Exception exception, string format, params object[] args);

        /// <summary>
        /// Logs a Warning Message
        /// </summary>
        /// <param name="provider">The string format, formatProvider.</param>
        /// <param name="format">The format of the string.</param>
        /// <param name="args">The format arguments.</param>
        void WarningFormat(IFormatProvider provider, string format, params object[] args);

        /// <summary>
        /// Logs a Warning Message
        /// </summary>
        /// <param name="exception">The Exception that is thrown.</param>
        /// <param name="provider">The provider used in the string format.</param>
        /// <param name="format">The string format.</param>
        /// <param name="args">The arguments.</param>
        void WarningFormat(Exception exception, IFormatProvider provider, string format, params object[] args);

        /// <summary>
        /// Logs a Fatal Message
        /// </summary>
        /// <param name="exception"></param>
        void Fatal(Exception exception);

        /// <summary>
        /// Logs a Fatal Message
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        void Fatal(Exception exception, string message);

        /// <summary>
        /// Logs a Fatal Message
        /// </summary>
        /// <param name="message"></param>
        void Fatal(string message);

        /// <summary>
        /// Logs a Fatal Message.
        /// </summary>
        /// <param name="format">The string format.</param>
        /// <param name="args">The arguments of the string format.</param>
        void FatalFormat(string format, params object[] args);

        /// <summary>
        /// Logs a Fatal Message.
        /// </summary>
        /// <param name="exception">The Exception that has been thrown</param>
        /// <param name="format">The format string.</param>
        /// <param name="args">The format arguments.</param>
        void FatalFormat(Exception exception, string format, params object[] args);

        /// <summary>
        /// Logs a Fatal Message
        /// </summary>
        /// <param name="provider">The string format, formatProvider.</param>
        /// <param name="format">The format of the string.</param>
        /// <param name="args">The format arguments.</param>
        void FatalFormat(IFormatProvider provider, string format, params object[] args);

        /// <summary>
        /// Logs a Fatal Message
        /// </summary>
        /// <param name="exception">The Exception that is thrown.</param>
        /// <param name="provider">The provider used in the string format.</param>
        /// <param name="format">The string format.</param>
        /// <param name="args">The arguments.</param>
        void FatalFormat(Exception exception, IFormatProvider provider, string format, params object[] args);

    }
}
