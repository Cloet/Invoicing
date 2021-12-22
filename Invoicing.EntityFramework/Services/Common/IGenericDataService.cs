using Invoicing.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework.Services.Common
{
    public interface IGenericDataService<T> : IReadOnlyDataService<T> where T : SQLModelBase<T>
    {
        public bool CreateMany(IEnumerable<T> entities);
        public Task<bool> CreateManyAsync(IEnumerable<T> entities);

        public T CreateOne(T entity);
        public Task<T> CreateOneAsync(T entity);

        public bool CreateOrUpdateMany(IEnumerable<T> entities);
        public Task<bool> CreateOrUpdateManyAsync(IEnumerable<T> entities);

        public T CreateOrUpdateOne(T entity);
        public Task<T> CreateOrUpdateOneAsync(T entity);

        public bool DeleteMany(IEnumerable<T> entities);
        public bool DeleteMany(Expression<Func<T, bool>> filter);

        public Task<bool> DeleteManyAsync(IEnumerable<T> entities);
        public Task<bool> DeleteManyAsync(Expression<Func<T, bool>> filter);

        public bool DeleteOne(T entity);
        public bool DeleteOne(int id);

        public Task<bool> DeleteOneAsync(T entity);
        public Task<bool> DeleteOneAsync(int id);

        public T UpdateOne(T entity);
        public Task<T> UpdateOneAsync(T entity);

    }
}
