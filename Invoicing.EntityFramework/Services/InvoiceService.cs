using Invoicing.Domain.Model;
using Invoicing.EntityFramework;
using Invoicing.EntityFramework.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework.Services
{
    public class InvoiceService : GenericDataService<Invoice, InvoicingDbContext>, IInvoiceService
    {

        public InvoiceService(IDbContextFactory<InvoicingDbContext> contextFactory) : base(contextFactory)
        {

        }
    }
}
