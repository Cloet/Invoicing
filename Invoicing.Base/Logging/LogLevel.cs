using System;
using System.Collections.Generic;
using System.Text;

namespace Invoicing.Base.Logging
{
    public enum LogLevel
    {
        Off = 0,
        Fatal = 1,
        Error = 2,
        Warning = 3,
        CrudInfo = 4,
        Information = 5,
        Debug = 6,
        Trace = 7,
        All = 8
    }
}
