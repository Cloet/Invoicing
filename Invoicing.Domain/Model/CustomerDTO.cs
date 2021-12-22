using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.Domain.Model
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string CustomerCode { get; set; }
        public string Name { get; set; }

        public Address Address { get; set; }

        public string Telephone { get; set; }

        public string Mobile { get; set; }

        public string EMail { get; set; }

        public string Website { get; set; }

    }
}
