using Invoicing.Base.Helpers;
using Invoicing.Base.Logging.Factory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace Invoicing.Base.Logging
{

    /// <inheritdoc cref="ICLogger"/>
    public abstract class LoggerBase : ICLogger
    {
        /// <summary>
        /// Name property
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Namespace property
        /// </summary>
        public string NameSpace { get; set; }

        /// <summary>
        /// The classname property
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// Shorter string of name property
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// File location
        /// </summary>
        public string Location { get; private set; }

        /// <summary>
        /// Separate logging in different files.
        /// </summary>
        public bool SeparateLogLevelFiles { get; private set; }

        /// <summary>
        /// Use subdirectories when writing to files.
        /// </summary>
        public bool SeparataSubDirectories { get; private set; }

        /// <summary>
        /// Check if extended messages should be logged.
        /// </summary>
        public bool ExtendedLogging { get; set; }

        /// <summary>
        /// Indicates the loglevel
        /// </summary>
        public LogLevel LoggerLevel { get; set; }

        internal LoggerBase() { }

        internal LoggerBase(string name, string location, bool separate, bool subDirs, bool extended, LogLevel level)
        {
            Name = name;
            Location = location;
            SeparateLogLevelFiles = separate;
            LoggerLevel = level;
            ExtendedLogging = extended;
            SeparataSubDirectories = subDirs;
        }

        //Gets the most suitable name for the loggers
        protected string GetName()
        {
            var name = Name;
            if (!string.IsNullOrEmpty(ShortName))
                name = ShortName;
            return name;
        }

        protected string GetNamespace()
        {
            return NameSpace.Substring(NameSpace.IndexOf(".", StringComparison.OrdinalIgnoreCase) + 1);
        }

        protected string TryGetClassName()
        {
            if (ClassName != null)
                return ClassName + ".cs";

            return string.Empty;
        }

        protected string GetLogLevel()
        {
            switch (LoggerLevel)
            {
                case LogLevel.Debug:
                    return "Debug";
                case LogLevel.Error:
                    return "Error";
                case LogLevel.Fatal:
                    return "Fatal";
                case LogLevel.Trace:
                    return "Trace";
                case LogLevel.Warning:
                    return "Warning";
                case LogLevel.Information:
                    return "Information";
                case LogLevel.CrudInfo:
                    return "DB Info";
                default:
                    return "Debug";
            }
        }

        private string CreateLogPathGeneric(string name, bool skipSeparateLogLevels = false)
        {
            var absolute = Path.GetPathRoot(Location);
            string baseDir;

            if (string.IsNullOrEmpty(absolute))
                baseDir = Directory.GetParent(Location) + Path.DirectorySeparatorChar.ToString() + Location + Path.DirectorySeparatorChar.ToString();
            else
            {
                baseDir = Path.GetFullPath(Location);
                if (!(baseDir.EndsWith(Path.DirectorySeparatorChar.ToString())))
                    baseDir += Path.DirectorySeparatorChar.ToString();
            }

            if (SeparateLogLevelFiles && !skipSeparateLogLevels)
            {
                if (SeparataSubDirectories && !string.IsNullOrEmpty(NameSpace))
                {
                    return baseDir + GetNamespace().Replace("_", Path.DirectorySeparatorChar.ToString()).Replace(".", Path.DirectorySeparatorChar.ToString()) + Path.DirectorySeparatorChar.ToString() + name + "." + GetLogLevel() + ".log";
                }
                else
                {
                    return baseDir + name + "." + GetLogLevel() + ".log";
                }
            }
            else
            {
                if (SeparataSubDirectories)
                {
                    return baseDir + GetNamespace().Replace("_", Path.DirectorySeparatorChar.ToString()).Replace(".", Path.DirectorySeparatorChar.ToString()) + Path.DirectorySeparatorChar.ToString() + name + ".log";
                }
                else
                {
                    return baseDir + name + ".log";
                }
            }
        }

        protected string CreateLogPath()
        {
            if (string.IsNullOrEmpty(NameSpace) || string.IsNullOrEmpty(ClassName))
                return CreateLogPathGeneric(GetName().Replace(".", "_"));

            if (!SeparataSubDirectories)
                return CreateLogPathGeneric(GetName().Replace(".", "_"));

            return CreateLogPathGeneric(ClassName);
        }

        protected string CreateLogPathObject(Type objType) => CreateLogPathGeneric(objType.Name.Replace(".", "_"), true);

        private string CreateOldLogObject(Type objType)
        {
            var i = 0;
            string dest;
            var objName = objType.Name.Replace(".", "_");
            do
            {
                i++;
                dest = CreateLogPathGeneric(objName + ".old" + i, true);
            } while (File.Exists(dest));

            return dest;
        }

        private string CreateOldLog()
        {
            var i = 0;
            string dest;
            string fileNameWithoutExtension;
            if (string.IsNullOrEmpty(NameSpace) || string.IsNullOrEmpty(ClassName))
                fileNameWithoutExtension = GetName().Replace(".", "_");
            else
                fileNameWithoutExtension = ClassName;
            var logLevel = GetLogLevel();

            do
            {
                i++;

                if (SeparateLogLevelFiles)
                    dest = CreateLogPathGeneric(fileNameWithoutExtension + "." + logLevel + ".old" + i, true);
                else
                    dest = CreateLogPathGeneric(fileNameWithoutExtension + ".old" + i, true);
            } while (File.Exists(dest));

            return dest;
        }

        protected virtual void WriteAndCheckIfMoved(string logMessage, LogLevel level)
        {
            try
            {
                var path = CreateLogPath();
                if (StreamWriter.WriteMessageToFile(path, logMessage))
                {
                    FileInfo fileInfo = new FileInfo(path);
                    var dest = CreateOldLog();
                    fileInfo.MoveTo(dest);
                }
            }
            catch (Exception)
            {
                // Do nothing...
                // Should only happen if file is inaccessible.
            }
        }

        protected virtual void WriteAndCheckIfMovedObject(string message, LogLevel level, Type objType)
        {
            try
            {
                var path = CreateLogPathObject(objType);
                if (StreamWriter.WriteMessageToFile(path, message))
                {
                    var info = new FileInfo(path);
                    var dest = CreateOldLogObject(objType);
                    info.MoveTo(dest);
                }
            }
            catch (Exception)
            {
                // Do nothing...
            }
        }


        protected int GetLineNumberCallingMember()
        {
            try
            {
                string lineSubString = Environment.StackTrace.Substring(Environment.StackTrace.LastIndexOf(' '));
                bool numeric = int.TryParse(lineSubString, out var line);

                if (numeric)
                    return line;

                StackTrace trace = new StackTrace();
                StackFrame[] frames = trace.GetFrames();

                if (frames != null)
                {
                    var frame = frames[frames.Length - 6];
                    line = frame.GetNativeOffset();
                    return line;
                }

                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        //Logs the messages
        protected virtual void Log(string message, Exception exception, LogLevel level)
        {
            try
            {
                var lineNumber = GetLineNumberCallingMember();
                var msg = MessageFactory.CreateMessage(ExtendedLogging, exception, DateTime.Now, message, level, GetName(), lineNumber, TryGetClassName());
                WriteAndCheckIfMoved(msg, level);
            }
            catch (Exception ex)
            {
                throw new LoggerException("Error trying to log a message with a Logger Named " + Name + "." + ex);
            }
        }

        protected virtual void LogObject<T>(string message, LogLevel level, T oldObj, T newObj)
        {
            if (oldObj == null && newObj == null)
                return;

            try
            {
                var lineNumber = GetLineNumberCallingMember();
                var msg = MessageFactory.CreateObjectMessage(message, oldObj, newObj, DateTime.Now, level, GetName(), lineNumber, TryGetClassName());

                // Only write something if there are changes.
                if (!string.IsNullOrEmpty(msg))
                {
                    Type objType = null;
                    if (oldObj == null)
                        objType = newObj.GetType();
                    else
                        objType = oldObj.GetType();

                    WriteAndCheckIfMovedObject(msg, level, objType);
                }

                // Subitems afhandelen naar correcte log files.
                ICollection<Tuple<object, object>> nonPrimitives;

                if (oldObj == null)
                    nonPrimitives = ObjectHelper.NonePrimitiveProperties(newObj, oldObj);
                else
                    nonPrimitives = ObjectHelper.NonePrimitiveProperties(oldObj, newObj);

                foreach (var item in nonPrimitives.OrEmptyIfNull())
                    TraceObjectSave(message, item.Item1, item.Item2);

            }
            catch (Exception ex)
            {
                throw new LoggerException("Error trying to log a message with a Logger named " + Name + "." + ex);
            }
        }

        private static LogLevel ConvertLogLevel(Microsoft.Extensions.Logging.LogLevel level)
        {
            switch (level)
            {
                case Microsoft.Extensions.Logging.LogLevel.Trace:
                    return LogLevel.Trace;
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                    return LogLevel.Debug;
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    return LogLevel.Information;
                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    return LogLevel.Warning;
                case Microsoft.Extensions.Logging.LogLevel.Error:
                    return LogLevel.Error;
                case Microsoft.Extensions.Logging.LogLevel.Critical:
                    return LogLevel.Fatal;
                case Microsoft.Extensions.Logging.LogLevel.None:
                    return LogLevel.Off;
                default:
                    return LogLevel.Debug;
            }
        }

        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            Log(formatter(state, exception), null, ConvertLogLevel(logLevel));
        }

        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            return ConvertLogLevel(logLevel) <= LoggerLevel;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        #region CRUD
        public void TraceObjectSave<T>(T oldObj, T newObj) => LogObject(null, LogLevel.CrudInfo, oldObj, newObj);
        public void TraceObjectSave<T>(string message, T oldObj, T newObj) => LogObject(message, LogLevel.CrudInfo, oldObj, newObj);

        public void TraceObjectCreation<T>(T createdObject) => LogObject(null, LogLevel.CrudInfo, default, createdObject);
        public void TraceObjectCreation<T>(string message, T createdObject) => LogObject(message, LogLevel.CrudInfo, default, createdObject);

        public void TraceObjectDeletion<T>(T deletedObject) => LogObject(null, LogLevel.CrudInfo, deletedObject, default);
        public void TraceObjectDeletion<T>(string message, T deletedObject) => LogObject(message, LogLevel.CrudInfo, deletedObject, default);
        #endregion

        #region Trace
        // Log a trace message
        public void Trace(Exception exception) => Log(null, exception, LogLevel.Trace);
        public void Trace(Exception exception, string message) => Log(message, exception, LogLevel.Trace);
        public void Trace(string message) => Log(message, null, LogLevel.Trace);

        // Log a trace message with custom formatting.
        public void TraceFormat(string format, params object[] args) => Log(string.Format(CultureInfo.CurrentCulture, format, args), null, LogLevel.Trace);
        public void TraceFormat(Exception exception, string format, params object[] args) => Log(string.Format(CultureInfo.CurrentCulture, format, args), exception, LogLevel.Trace);
        public void TraceFormat(IFormatProvider provider, string format, params object[] args) => Log(string.Format(provider, format, args), null, LogLevel.Trace);
        public void TraceFormat(Exception exception, IFormatProvider provider, string format, params object[] args) => Log(string.Format(provider, format, args), exception, LogLevel.Trace);
        #endregion

        #region Error
        public void Error(Exception exception) => Log(null, exception, LogLevel.Error);
        public void Error(Exception exception, string message) => Log(message, exception, LogLevel.Error);
        public void Error(string message) => Log(message, null, LogLevel.Error);

        public void ErrorFormat(string format, params object[] args) => Log(string.Format(CultureInfo.CurrentCulture, format, args), null, LogLevel.Error);
        public void ErrorFormat(Exception exception, string format, params object[] args) => Log(string.Format(CultureInfo.CurrentCulture, format, args), exception, LogLevel.Error);
        public void ErrorFormat(IFormatProvider provider, string format, params object[] args) => Log(string.Format(provider, format, args), null, LogLevel.Error);
        public void ErrorFormat(Exception exception, IFormatProvider provider, string format, params object[] args) => Log(string.Format(provider, format, args), exception, LogLevel.Error);
        #endregion

        #region Information
        public void Info(Exception exception) => Log(null, exception, LogLevel.Information);
        public void Info(Exception exception, string message) => Log(message, exception, LogLevel.Information);
        public void Info(string message) => Log(message, null, LogLevel.Information);

        public void InfoFormat(string format, params object[] args) => Log(string.Format(CultureInfo.CurrentCulture, format, args), null, LogLevel.Information);
        public void InfoFormat(Exception exception, string format, params object[] args) => Log(string.Format(CultureInfo.CurrentCulture, format, args), exception, LogLevel.Information);
        public void InfoFormat(IFormatProvider provider, string format, params object[] args) => Log(string.Format(provider, format, args), null, LogLevel.Information);
        public void InfoFormat(Exception exception, IFormatProvider provider, string format, params object[] args) => Log(string.Format(provider, format, args), exception, LogLevel.Information);
        #endregion

        #region Debug
        public void Debug(Exception exception) => Log(null, exception, LogLevel.Debug);
        public void Debug(Exception exception, string message) => Log(message, exception, LogLevel.Debug);
        public void Debug(string message) => Log(message, null, LogLevel.Debug);

        public void DebugFormat(string format, params object[] args) => Log(string.Format(CultureInfo.CurrentCulture, format, args), null, LogLevel.Debug);
        public void DebugFormat(Exception exception, string format, params object[] args) => Log(string.Format(CultureInfo.CurrentCulture, format, args), exception, LogLevel.Debug);
        public void DebugFormat(IFormatProvider provider, string format, params object[] args) => Log(string.Format(provider, format, args), null, LogLevel.Debug);
        public void DebugFormat(Exception exception, IFormatProvider provider, string format, params object[] args) => Log(string.Format(provider, format, args), exception, LogLevel.Debug);
        #endregion

        #region Warning
        public void Warning(Exception exception) => Log(null, exception, LogLevel.Warning);
        public void Warning(Exception exception, string message) => Log(message, exception, LogLevel.Warning);
        public void Warning(string message) => Log(message, null, LogLevel.Warning);

        public void WarningFormat(string format, params object[] args) => Log(string.Format(CultureInfo.CurrentCulture, format, args), null, LogLevel.Warning);
        public void WarningFormat(Exception exception, string format, params object[] args) => Log(string.Format(CultureInfo.CurrentCulture, format, args), exception, LogLevel.Warning);
        public void WarningFormat(IFormatProvider provider, string format, params object[] args) => Log(string.Format(provider, format, args), null, LogLevel.Warning);
        public void WarningFormat(Exception exception, IFormatProvider provider, string format, params object[] args) => Log(string.Format(provider, format, args), exception, LogLevel.Warning);
        #endregion

        #region Fatal
        public void Fatal(Exception exception) => Log(null, exception, LogLevel.Fatal);
        public void Fatal(Exception exception, string message) => Log(message, exception, LogLevel.Fatal);
        public void Fatal(string message) => Log(message, null, LogLevel.Fatal);

        public void FatalFormat(string format, params object[] args) => Log(string.Format(CultureInfo.CurrentCulture, format, args), null, LogLevel.Fatal);
        public void FatalFormat(Exception exception, string format, params object[] args) => Log(string.Format(CultureInfo.CurrentCulture, format, args), exception, LogLevel.Fatal);
        public void FatalFormat(IFormatProvider provider, string format, params object[] args) => Log(string.Format(provider, format, args), null, LogLevel.Fatal);
        public void FatalFormat(Exception exception, IFormatProvider provider, string format, params object[] args) => Log(string.Format(provider, format, args), exception, LogLevel.Fatal);
        #endregion
    }
}
