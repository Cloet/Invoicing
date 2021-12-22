using Invoicing.Domain.Model;
using Invoicing.Domain.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework.Services
{
    public interface IUserService : IReadOnlyDataService<User>
    {
    }
}
