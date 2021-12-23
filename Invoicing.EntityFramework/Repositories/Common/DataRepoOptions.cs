using System;
using System.Collections.Generic;
using System.Text;

namespace Invoicing.EntityFramework.Repositories.Common
{
    public class DataRepoOptions
    {

        public bool IsReadOnly { get; set; } = false;

        public bool AsNoTracking { get; set; } = false;

        public DataRepoOptions()
        {

        }

    }
}
