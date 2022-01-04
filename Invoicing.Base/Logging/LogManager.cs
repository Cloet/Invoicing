using Invoicing.Base.Logging.Factory;
using System;
using System.Collections.Generic;

namespace Invoicing.Base.Logging
{
    public class LogManager
    {
        private static CLoggerFactory Factory;
        private static readonly List<ICLogger> Loggers = new List<ICLogger>();

        public static void InitializeLogManager(LoggerConfig settings)
        {
            Factory = new CLoggerFactory(settings);
        }

        public static ICLogger GetLogger(string name)
        {
            try
            {
                if (Factory == null)
                    InitializeLogManager(LoggerConfig.DefaultSettings);

                ICLogger logger = Loggers.Find(x => x.Name == name);

                if (logger != null)
                    return logger;

                logger = Factory.Create(name);

                return logger;
            }
            catch (Exception ex)
            {
                throw new LoggerException("Error getting or creating a logger from the LogManager.", ex);
            }
        }

        public static ICLogger GetLogger(Type type)
        {
            Logger logger = (Logger)GetLogger(type.FullName);

            if (type.Namespace != null)
            {
                var name = type.Namespace.Substring(type.Namespace.IndexOf(".", StringComparison.OrdinalIgnoreCase) + 1);
                logger.ShortName = name + "." + type.Name;
            }
            else
                logger.ShortName = type.FullName;

            logger.NameSpace = type.Namespace;
            logger.ClassName = type.Name;

            return logger;
        }

        public static bool LoggerExists(string name)
        {
            try
            {
                return Loggers.Exists(x => x.Name == name);
            }
            catch (Exception)
            {
                throw new LoggerException("Error trying to determine if a logger exists in the LogManager.");
            }
        }

        internal static void AddLogger(ICLogger logger)
        {
            try
            {
                Loggers.Add(logger);
            }
            catch (Exception ex)
            {
                throw new LoggerException("Error trying to add a new logger...", ex);
            }
        }

    }
}
