using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Invoicing.Domain.Model.Common;

namespace Invoicing.Domain.Model
{
    public class InvoiceLine : SQLModelBase<InvoiceLine>
    {

        [Required]
        public int InvoiceNumber { get; set; }

        [Required]
        public float LineNumber { get; set; }

        [Required]
        public string ArticleCode { get; set; }

        public string ArticleDescription { get; set; }

        public float Quantity { get; set; }

        public double UnitPrice { get; set; }

        public string VATCode { get; set; }

        public double VATPercentage { get; set; }

    }
}
