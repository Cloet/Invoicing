using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey("CountryID")]
        public Country Country { get; set; }

        public int CountryID { get; set; }

    }
}
