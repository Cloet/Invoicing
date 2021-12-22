using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Invoicing.Domain.Model.Common;

namespace Invoicing.Domain.Model
{
    public class Article : SQLModelBase<Article>
    {

        [Required]
        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public double UnitPrice { get; set; }

        public VAT VAT { get; set; }

        public double UnitPriceIncludingVAT => UnitPrice * (1 + VAT.Percentage / 100);

    }
}
