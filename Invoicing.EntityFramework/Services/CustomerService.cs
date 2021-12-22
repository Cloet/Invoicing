﻿using Invoicing.Domain.Model;
using Invoicing.EntityFramework.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework.Services
{
    public class CustomerService : GenericDataService<Customer, InvoicingDbContext>, ICustomerService
    {

        public CustomerService(IDbContextFactory<InvoicingDbContext> contextFactory) : base(contextFactory)
        {

        }

    }
}
