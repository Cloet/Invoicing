using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Invoicing.Domain.Model.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Invoicing.Domain.Model
{
    public class Article : SQLModelBase<Article>
    {

        [Required]
        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public double UnitPrice { get; set; }

        [ForeignKey("VATId")]
        public VAT VAT { get; set; }

        public double UnitPriceIncludingVAT { get; set; }

        [Required]
        public int VATId { get; set; }

    }
}
