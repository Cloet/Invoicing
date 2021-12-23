using Invoicing.Domain.Model;
using Invoicing.EntityFramework.Repositories.Common;

namespace Invoicing.EntityFramework.Repositories
{
    public interface IUserRepository : IGenericDataRepository<User>
    {
    }
}
