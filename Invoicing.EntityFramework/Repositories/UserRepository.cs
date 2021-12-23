using Invoicing.Domain.Model;
using Invoicing.EntityFramework.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework.Repositories
{
    public class UserRepository : GenericDataRepository<User, InvoicingDbContext>, IUserRepository
    {

        public UserRepository(IDbContextFactory<InvoicingDbContext> contextFactory) : base(contextFactory)
        {

        }

        public User GetByUserName(string username)
        {
            try
            {
                return Filter(x => x.Username == username).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _datarepositoryLogger.Error(ex);
                throw;
            }

        }


    }
}
