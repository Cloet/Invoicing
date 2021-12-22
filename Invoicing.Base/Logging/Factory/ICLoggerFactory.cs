using System;
using System.Collections.Generic;
using System.Text;

namespace Invoicing.Base.Logging.Factory
{
    internal interface ICLoggerFactory
    {
        ICLogger Create(string name);
        ICLogger Create(Type type);
    }
}
