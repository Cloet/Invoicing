using System;
using System.Collections.Generic;
using System.Text;

namespace Invoicing.Base.Logging.Factory
{
    internal class CLoggerFactory : ICLoggerFactory
    {

        private readonly LoggerConfig _config = null;

        internal CLoggerFactory(LoggerConfig config)
        {
            _config = config;
        }

        private static void LoggerExists(string name)
        {
            if (LogManager.LoggerExists(name))
            {
                throw new LoggerException("A Logger with this name already exists...");
            }
        }

        public ICLogger Create(string name)
        {
            LoggerExists(name);

            try
            {
                ICLogger logger = new Logger(name, _config.BaseDir, _config.SeparateLogLevelFiles, _config.SeparateSubDirectories, _config.ExtendedLogging, _config.LoggerLevel);

                LogManager.AddLogger(logger);
                return logger;
            }
            catch (Exception ex)
            {
                throw new LoggerException("Error creating a new logger...", ex);
            }
        }
        public ICLogger Create(Type type) => Create(type.FullName);
    }
}
