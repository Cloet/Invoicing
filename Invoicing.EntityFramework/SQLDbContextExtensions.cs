using Invoicing.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework
{
    public static class SQLDbContextExtensions
    {

        public static bool IsTracked<TContext, TEntity>(this TContext context, TEntity entity) where TContext : SQLDbContext where TEntity : SQLModelBase<TEntity>
        {
            return context.Set<TEntity>().Local.Any(e => e == entity);
        }

        public static bool IsItNew<TContext, TEntity>(this TContext context, TEntity entity) where TContext : SQLDbContext where TEntity : SQLModelBase<TEntity>
        {
            return !context.Entry(entity).IsKeySet;
        }


    }
}
