using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invoicing.Base.Logging.Factory
{
    public class CNetLoggerFactory : ILoggerFactory
    {
        public void AddProvider(ILoggerProvider provider)
        {
            // Ignored
        }

        public ILogger CreateLogger(string categoryName)
        {
            return LogManager.GetLogger(categoryName);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
