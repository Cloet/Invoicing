using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Invoicing.Domain.Model.Common;

namespace Invoicing.Domain.Model
{
    public class VAT : SQLModelBase<VAT>
    {

        [Required]
        public string Code { get; set; }

        public double Percentage { get; set; }

        public string Description { get; set; }

    }
}
