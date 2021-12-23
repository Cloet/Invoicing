using Invoicing.Domain.Model;
using Invoicing.EntityFramework.Repositories.Common;

namespace Invoicing.EntityFramework.Repositories
{
    public class AddressRepository : GenericDataRepository<Address, InvoicingDbContext>, IAddressRepository
    {

        public AddressRepository(IDbContextFactory<InvoicingDbContext> contextFactory) : base(contextFactory)
        {

        }

    }
}
