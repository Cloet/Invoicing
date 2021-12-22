using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invoicing.Base.Logging.Extensions
{
    public static class ConfigureExtensions
    {

        public static ILoggerFactory AddCLogger(this ILoggerFactory factory)
        {
            factory.AddProvider(new CLoggerProvider());
            return factory;
        }

        public static ILoggingBuilder AddCLogger(this ILoggingBuilder factory)
        {
            factory.AddProvider(new CLoggerProvider());
            return factory;
        }

    }
}
