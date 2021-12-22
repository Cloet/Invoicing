using System;
using System.Collections.Generic;
using System.Text;

namespace Invoicing.EntityFramework
{
    public interface IDbContextFactory<T> where T : SQLDbContext
    {
        T CreateDBContext();
    }
}
