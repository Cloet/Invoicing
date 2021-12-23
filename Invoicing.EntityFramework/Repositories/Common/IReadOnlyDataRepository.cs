using Invoicing.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework.Repositories.Common
{
    public interface IReadOnlyDataRepository<T> where T : SQLModelBase<T>
    {
        public IQueryable<T> Filter(Expression<Func<T, bool>> filter, int results);
        public Task<IQueryable<T>> FilterAsync(Expression<Func<T, bool>> filter, int results);
        public IQueryable<T> GetAll();
        public Task<IQueryable<T>> GetAllAsync();
        public T GetOne(int id);
        public Task<T> GetOneAsync(int id);

    }
}