using System;
using System.Collections.Generic;
using System.Text;

namespace Invoicing.Base.Logging
{
    public class LoggerConfig
    {
        public bool ExtendedLogging { get; set; }
        public string BaseDir { get; set; }
        public bool SeparateLogLevelFiles { get; set; }
        public bool SeparateSubDirectories { get; set; }
        public LogLevel LoggerLevel { get; set; }

        public static LoggerConfig DefaultSettings
        {
            get
            {
                return new LoggerConfig
                {
                    ExtendedLogging = false,
                    BaseDir = "",
                    SeparateLogLevelFiles = false,
                    SeparateSubDirectories = false,
                    LoggerLevel = LogLevel.Information
                };
            }
        }

    }
}
