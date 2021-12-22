using System;
using System.Collections.Generic;
using System.Text;

namespace Invoicing.Base.Logging
{
    public class Logger : LoggerBase
    {
        internal Logger() : base()
        {

        }

        internal Logger(string name, string location, bool separate, bool useSubDirs, bool extended, LogLevel level) : base(name, location, separate, useSubDirs, extended, level)
        {

        }

    }
}
