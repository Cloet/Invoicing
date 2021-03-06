using Invoicing.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Invoicing.Domain.Model
{
    public class Address : SQLModelBase<Address>
    {

        [Required]
        public string Name { get; set; }

        public string Number { get; set; }

        public string Street { get; set; }

        public City City { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [Required]
        public int CustomerId { get; set; }

    }
}
