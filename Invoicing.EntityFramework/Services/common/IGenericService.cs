using Invoicing.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework.Services.common
{
    public interface IGenericService<T> : IDisposable where T : SQLModelBase<T>
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
