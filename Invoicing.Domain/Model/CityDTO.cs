using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.Domain.Model
{
    public class CityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool MainMunicipality { get; set; }

        public string Postal { get; set; }

        public CountryDTO Country { get; set; }

    }
}
