using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.Domain.Model
{
    public class ArticleDTO
    {
        public int Id { get; set; }

        public string ArticleCode { get; set; }

        public string Description { get; set; }

        public double UnitPrice { get; set; }


    }
}
