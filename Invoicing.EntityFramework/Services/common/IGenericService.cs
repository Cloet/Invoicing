using Invoicing.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework.Services.common
{
    public interface IGenericService<T> where T : SQLModelBase<T>
    {
        public IEnumerable<T> Filter(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int take = -1, int skip = -1);

        public IEnumerable<T> Filter(Expression<Func<T, bool>> filter, int take = -1, int skip = -1);

        public IEnumerable<T> Filter(Expression<Func<T, bool>> filter);

        public Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int take = -1, int skip = -1);

        public Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> filter, int take = -1, int skip = -1);

        public Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> filter);

        public T FirstRecord();
        public Task<T> FirstRecordAsync();
        public IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        public Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        public T GetOne(int id);
        public Task<T> GetOneAsync(int id);
        public T LastRecord();
        public Task<T> LastRecordAsync();
        public T NextRecord(T current);
        public Task<T> NextRecordAsync(T current);
        public T PreviousRecord(T current);
        public Task<T> PreviousRecordAsync(T current);
        public bool RecordExists(int id);
        public Task<bool> RecordExistsAsync(int id);

        public bool CreateMany(IEnumerable<T> entities);
        public Task<bool> CreateManyAsync(IEnumerable<T> entities);

        public T CreateOne(T entity);
        public Task<T> CreateOneAsync(T entity);

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
