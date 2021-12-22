using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.Domain.Model
{
    public class InvoiceDTO
    {
        public int Id { get; set; }
        public int InvoiceNumber { get; set; }

        public string CustomerName { get; set; }

        public string CustomerCode { get; set; }

        public string AddressName { get; set; }

        public string AddressStreet { get; set; }

        public City AddressCity { get; set; }

        public double TotalExcludingVAT { get; set; }

        public double TotalVAT { get; set; }

        public double TotalIncludingVAT { get; set; }


    }
}
