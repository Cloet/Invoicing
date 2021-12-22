using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.Domain.Model
{
    public class AddressDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Number { get; set; }

        public string Street { get; set; }

        public City City { get; set; }
    }
}
