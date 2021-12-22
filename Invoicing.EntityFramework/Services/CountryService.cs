using Invoicing.Domain.Model;
using Invoicing.EntityFramework.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework.Services
{
    public class CountryService : GenericDataService<Country, InvoicingDbContext>, ICountryService
    {

        public CountryService(IDbContextFactory<InvoicingDbContext> contextFactory) : base(contextFactory)
        {

        }

    }
}
