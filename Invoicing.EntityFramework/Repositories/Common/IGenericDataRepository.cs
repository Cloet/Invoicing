using Invoicing.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework.Repositories.Common
{
    public interface IGenericDataRepository<T> : IReadOnlyDataRepository<T> where T : SQLModelBase<T>
    {
        public TEntity Detach<TEntity>(TEntity entity);

        public void CreateMany(IEnumerable<T> entities);
        public Task CreateManyAsync(IEnumerable<T> entities);

        public void CreateOne(T entity);
        public Task CreateOneAsync(T entity);

        public void DeleteMany(IEnumerable<T> entities);
        public void DeleteMany(Expression<Func<T, bool>> filter);

        public Task DeleteManyAsync(IEnumerable<T> entities);
        public Task DeleteManyAsync(Expression<Func<T, bool>> filter);

        public void DeleteOne(T entity);
        public void DeleteOne(int id);

        public Task DeleteOneAsync(T entity);
        public Task DeleteOneAsync(int id);

        public void UpdateOne(T entity);
        public Task UpdateOneAsync(T entity);

        public void Save();

        public Task SaveAsync();


    }
}
