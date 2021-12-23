using Invoicing.Domain.Model;
using Invoicing.EntityFramework.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework.Repositories
{
    public class CityRepository : GenericDataRepository<City, InvoicingDbContext>, ICityRepository
    {

        public CityRepository(IDbContextFactory<InvoicingDbContext> contextFactory) : base(contextFactory)
        {
        }

    }
}
