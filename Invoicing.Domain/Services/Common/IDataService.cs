using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.Domain.Services.Common
{
    public interface IDataService<T> : IReadOnlyDataService<T>
    {

        Task<T> CreateOneAsync(T entity);
        T CreateOne(T entity);

        Task<bool> CreateManyAsync(IEnumerable<T> entities);
        bool CreateMany(IEnumerable<T> entities);

        Task<T> UpdateOneAsync(T entity);
        T UpdateOne(T entity);

        Task<bool> DeleteOneAsync(int id);
        bool DeleteOne(int id);

        Task<bool> DeleteManyAsync(Expression<Func<T, bool>> filter);
        bool DeleteMany(Expression<Func<T, bool>> filter);

        Task<bool> CreateOrUpdateManyAsync(IEnumerable<T> entities);
        bool CreateOrUpdateMany(IEnumerable<T> entities);

        Task<T> CreateOrUpdateOneAsync(T entity);
        T CreateOrUpdateOne(T entity);

    }
}
