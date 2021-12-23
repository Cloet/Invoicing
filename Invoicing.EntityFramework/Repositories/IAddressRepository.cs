using Invoicing.Domain.Model;
using Invoicing.EntityFramework.Repositories.Common;

namespace Invoicing.EntityFramework.Repositories
{
    public interface IAddressRepository : IGenericDataRepository<Address>
    {
    }
}
