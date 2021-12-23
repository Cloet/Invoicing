using Invoicing.Base.Logging;
using Invoicing.Domain.Model.Common;
using Invoicing.EntityFramework.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework.Services.common
{
    public abstract class GenericService<T> : IGenericService<T> where T : SQLModelBase<T>
    {

        protected readonly IGenericDataRepository<T> _repository;
        protected readonly ICLogger _servicelogger;

        public GenericService(IGenericDataRepository<T> repository)
        {
            _repository = repository;
            _servicelogger = LogManager.GetLogger(GetType());
        }

        public virtual bool CreateMany(IEnumerable<T> entities)
        {
            return _repository.CreateMany(entities);
        }
        public virtual async Task<bool> CreateManyAsync(IEnumerable<T> entities)
        {
            return await _repository.CreateManyAsync(entities);
        }

        public T CreateOne(T entity)
        {
            return _repository.CreateOne(entity);
        }
        public virtual async Task<T> CreateOneAsync(T entity)
        {
            return await _repository.CreateOneAsync(entity);
        }

        public virtual bool DeleteMany(IEnumerable<T> entities)
        {
            return _repository.DeleteMany(entities);
        }
        public virtual bool DeleteMany(Expression<Func<T, bool>> filter)
        {
            return _repository.DeleteMany(filter);
        }
        public virtual async Task<bool> DeleteManyAsync(IEnumerable<T> entities)
        {
            return await _repository.DeleteManyAsync(entities);
        }
        public virtual async Task<bool> DeleteManyAsync(Expression<Func<T, bool>> filter) => await DeleteManyAsync(await FilterAsync(filter));
        public virtual bool DeleteOne(T entity) => _repository.DeleteOne(entity.Id);
        public virtual bool DeleteOne(int id)
        {
            return _repository.DeleteOne(id);
        }

        public virtual async Task<bool> DeleteOneAsync(T entity) => await _repository.DeleteOneAsync(entity.Id);

        public virtual async Task<bool> DeleteOneAsync(int id)
        {
            return await _repository.DeleteOneAsync(id);
        }

        public virtual IEnumerable<T> Filter(Expression<Func<T, bool>> filter) => _repository.Filter(filter, -1);

        public virtual IEnumerable<T> Filter(Expression<Func<T, bool>> filter, int results)
        {
            return _repository.Filter(filter, results);
        }

        public virtual async Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> filter) => await _repository.FilterAsync(filter, -1);

        public virtual async Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> filter, int results)
        {
            return await _repository.FilterAsync(filter, results);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual T GetOne(int id)
        {
            return _repository.GetOne(id);
        }

        public virtual async Task<T> GetOneAsync(int id)
        {
            return await _repository.GetOneAsync(id);
        }

        public virtual T UpdateOne(T entity)
        {
            return _repository.UpdateOne(entity);
        }

        public virtual async Task<T> UpdateOneAsync(T entity)
        {
            return await _repository.UpdateOneAsync(entity);
        }

        public virtual T FirstRecord()
        {
            return _repository.GetAll().OrderBy(x => x.Id).FirstOrDefault();
        }

        public virtual async Task<T> FirstRecordAsync()
        {
            return await Task.Run(() => FirstRecord());
        }


        public virtual T LastRecord()
        {
            return _repository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault();
        }

        public virtual async Task<T> LastRecordAsync()
        {
            return await Task.Run(() => LastRecord());
        }

        public virtual T NextRecord(T current)
        {
            if (current == null || current.IsNew())
                return LastRecord();

            var next = Filter(x => x.Id > current.Id).OrderBy(x => x.Id).FirstOrDefault();

            if (next == null)
                return current;

            return next;
        }

        public virtual async Task<T> NextRecordAsync(T current)
        {
            return await Task.Run(() => NextRecord(current));
        }

        public virtual T PreviousRecord(T current)
        {
            var prev = Filter(x => x.Id < current.Id).OrderByDescending(x => x.Id).FirstOrDefault();

            if (prev == null)
                return current;

            return prev;
        }

        public virtual async Task<T> PreviousRecordAsync(T current)
        {
            return await Task.Run(() => PreviousRecord(current));
        }

        public virtual bool RecordExists(int id)
        {
            var item = GetOne(id);
            return item != null;
        }

        public virtual async Task<bool> RecordExistsAsync(int id)
        {
            var item = await GetOneAsync(id);
            return item != null;
        }

    }
}
