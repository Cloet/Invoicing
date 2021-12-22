using System;
using System.Collections.Generic;
using System.Text;

namespace Invoicing.Domain.Model.Common
{
    public interface ISQLObject
    {

        /// <summary>
        /// Unique identifier
        /// </summary>
        int Id { get; set; }

    }
}
