using System;
using System.Collections.Generic;
using System.Text;

namespace Invoicing.Base.Logging
{
    internal class LoggerException : Exception
    {
        public LoggerException() { }

        public LoggerException(string message) : base(message) { }

        public LoggerException(string message, Exception innerException) : base(message, innerException) { }

    }
}
