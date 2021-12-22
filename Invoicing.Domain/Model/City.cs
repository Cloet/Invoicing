using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Invoicing.Domain.Model.Common;

namespace Invoicing.Domain.Model
{
    public class City : SQLModelBase<City>
    {

        [Required]
        public string Name { get; set; }

        public bool MainMunicipality { get; set; }

        [Required]
        public string Postal { get; set; }

        [Required]
        public Country Country { get; set; }

        private City()
        {

        }

        public City(string name, string postal, Country country, bool mainMunicipality = false)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));
            if (string.IsNullOrEmpty(postal))
                throw new ArgumentException(nameof(postal));
            if (country == null)
                throw new ArgumentException(nameof(country));

            Name = name;
            Postal = postal;
            Country = country;
            MainMunicipality = mainMunicipality;

        }

    }
}
