using Invoicing.Domain.Model;
using Invoicing.EntityFramework.Services.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework.Services
{
    public interface ICustomerService: IGenericService<Customer>
    {
    }
}
