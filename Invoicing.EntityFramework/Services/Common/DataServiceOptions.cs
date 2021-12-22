using System;
using System.Collections.Generic;
using System.Text;

namespace Invoicing.EntityFramework.Services.Common
{
    public class DataServiceOptions
    {

        public bool IsReadOnly { get; set; } = false;

        public bool AsNoTracking { get; set; } = false;

        public DataServiceOptions()
        {

        }

    }
}
