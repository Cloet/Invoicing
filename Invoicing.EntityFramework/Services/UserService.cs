using Invoicing.Domain.Model;
using Invoicing.EntityFramework.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework.Services
{
    public class UserService : ReadOnlyGenericDataService<User, InvoicingDbContext>, IUserService
    {

        public UserService(IDbContextFactory<InvoicingDbContext> contextFactory) : base(contextFactory)
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
                _dataserviceLogger.Error(ex);
                throw;
            }

        }


    }
}
