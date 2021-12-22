using System;
using System.Collections.Generic;
using System.Text;

namespace Invoicing.Base.Helpers
{
    public class PropertyDifferences
    {
        public string Property { get; set; }

        public object OldVal { get; set; }

        public object NewVal { get; set; }
    }
}
