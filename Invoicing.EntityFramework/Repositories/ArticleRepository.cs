using Invoicing.Domain.Model;
using Invoicing.EntityFramework.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework.Repositories
{
    public class ArticleRepository : GenericDataRepository<Article, InvoicingDbContext>, IArticleRepository
    {

        public ArticleRepository(IDbContextFactory<InvoicingDbContext> contextFactory) : base(contextFactory)
        {

        }

        protected override IQueryable<Article> QueryIncludes(IQueryable<Article> query)
        {
            query = base.QueryIncludes(query);
            return query.Include(a => a.VAT);
        }

    }
}
