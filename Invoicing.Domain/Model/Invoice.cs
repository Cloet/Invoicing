using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Invoicing.Domain.Model.Common;

namespace Invoicing.Domain.Model
{
    public class Invoice : SQLModelBase<Invoice>
    {

        [Required]
        public int InvoiceNumber { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string CustomerCode { get; set; }

        [Required]
        public string AddressName { get; set; }

        [Required]
        public string AddressStreet { get; set; }

        [Required]
        public City AddressCity { get; set; }

        public double TotalExcludingVAT { get; set; }

        public double TotalVAT { get; set; }

        public double TotalIncludingVAT { get; set; }

        public IList<InvoiceLine> InvoiceLines { get; set; }

    }
}
