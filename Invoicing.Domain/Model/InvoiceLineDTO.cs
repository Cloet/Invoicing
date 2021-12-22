using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.Domain.Model
{
    public class InvoiceLineDTO
    {
        public int Id { get; set; }
        public int InvoiceNumber { get; set; }

        public float LineNumber { get; set; }

        public string ArticleCode { get; set; }

        public string ArticleDescription { get; set; }

        public float Quantity { get; set; }

        public double UnitPrice { get; set; }

        public string VATCode { get; set; }

        public double VATPercentage { get; set; }

    }
}
