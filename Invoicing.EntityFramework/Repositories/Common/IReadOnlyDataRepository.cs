using Invoicing.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework.Repositories.Common
{
    public interface IReadOnlyDataRepository<T> : IDisposable where T : SQLModelBase<T>
    {
        public Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int take = -1, int skip = -1);
        public IEnumerable<T> Filter(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int take = -1, int skip = -1);
        public IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        public Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        public T GetOne(int id);
        public Task<T> GetOneAsync(int id);


    }
}