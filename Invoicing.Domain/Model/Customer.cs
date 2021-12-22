using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Invoicing.Domain.Model.Common;

namespace Invoicing.Domain.Model
{
    public class Customer : SQLModelBase<Customer>
    {
        [Required]
        public string CustomerCode { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Address_Id")]
        public Address Address { get; set; }

        public string Telephone { get; set; }

        public string Mobile { get; set; }

        public string EMail { get; set; }

        public string Website { get; set; }

        public new static Customer Empty
        {
            get
            {
                var cust = new Customer();
                cust.Address = new Address();
                return cust;
            }
        }

    }
}
