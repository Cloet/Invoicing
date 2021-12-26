using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.Domain.Model
{
    public class VatDTO
    {

        public int Id { get; set; }
        public string Code { get; set; }

        public double Percentage { get; set; }

        public string Description { get; set; }

    }
}
