using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Invoicing.Domain.Model.Common;

namespace Invoicing.Domain.Model
{
    public class Country : SQLModelBase<Country>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^[A-Z _-]*")]
        public string CountryCode { get; set; }

    }
}
