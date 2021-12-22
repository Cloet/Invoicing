using Invoicing.Domain.Model;
using Invoicing.EntityFramework.Services.Common;

namespace Invoicing.EntityFramework.Services
{
    public class AddressService : GenericDataService<Address, InvoicingDbContext>, IAddressService
    {

        public AddressService(IDbContextFactory<InvoicingDbContext> contextFactory) : base(contextFactory)
        {

        }

    }
}
