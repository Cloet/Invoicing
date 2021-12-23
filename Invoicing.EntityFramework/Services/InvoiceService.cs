using Invoicing.Domain.Model;
using Invoicing.EntityFramework.Repositories;
using Invoicing.EntityFramework.Services.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework.Services
{
    public class InvoiceService: GenericService<Invoice>, IInvoiceService
    {
        public InvoiceService(IInvoiceRepository repository): base(repository)
        {

        }

    }
}
