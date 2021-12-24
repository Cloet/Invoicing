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

        protected virtual T BeforeInsertUpdate(T entity)
        {
            return entity;
        }

        private IEnumerable<T> BeforeInsertUpdateMany(IEnumerable<T> entities)
        {
            foreach(var entity in entities)
            {
                yield return BeforeInsertUpdate(entity);
            }
        }

        public virtual bool CreateMany(IEnumerable<T> entities)
        {
            entities = BeforeInsertUpdateMany(entities);
            return _repository.CreateMany(entities);
        }
        public virtual async Task<bool> CreateManyAsync(IEnumerable<T> entities)
        {
            entities = BeforeInsertUpdateMany(entities);
            return await _repository.CreateManyAsync(entities);
        }

        public virtual T CreateOne(T entity)
        {
            entity = BeforeInsertUpdate(entity);
            return _repository.CreateOne(entity);
        }
        public virtual async Task<T> CreateOneAsync(T entity)
        {
            entity = BeforeInsertUpdate(entity);
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
        public virtual async Task<bool> DeleteManyAsync(Expression<Func<T, bool>> filter)
        {
            return await DeleteManyAsync(await FilterAsync(filter));
        }

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

        public IEnumerable<T> Filter(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int take = -1, int skip = -1)
        {
            return _repository.Filter(filter, orderBy, skip, take);
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> filter, int take = -1, int skip = -1) {
            return Filter(filter, null, take, skip);
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> filter)
        {
            return Filter(filter, null, -1, -1);
        }

        public async Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int take = -1, int skip = -1)
        {
            return await _repository.FilterAsync(filter, orderBy, skip, take);
        }

        public async Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> filter, int take = -1, int skip = -1)
        {
            return await FilterAsync(filter, null, take, skip);
        }

        public async Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> filter)
        {
            return await FilterAsync(filter, null, -1, -1);
        }

        public virtual IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            return _repository.GetAll(orderBy);
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            return await _repository.GetAllAsync(orderBy);
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
            entity = BeforeInsertUpdate(entity);
            return _repository.UpdateOne(entity);
        }
        public virtual async Task<T> UpdateOneAsync(T entity)
        {
            entity = BeforeInsertUpdate(entity);
            return await _repository.UpdateOneAsync(entity);
        }

        public virtual T FirstRecord()
        {
            return _repository.Filter(null, x => x.OrderBy(x => x.Id), 0, 1).FirstOrDefault();
        }
        public virtual async Task<T> FirstRecordAsync()
        {
            var items = await _repository.FilterAsync(null, x => x.OrderBy(x => x.Id), 0, 1);
            return items.FirstOrDefault();
        }


        public virtual T LastRecord()
        {
            return _repository.Filter(null, x => x.OrderByDescending(x => x.Id), 0, 1).FirstOrDefault();
        }

        public virtual async Task<T> LastRecordAsync()
        {
            var items = await _repository.FilterAsync(null, x => x.OrderByDescending(x => x.Id), 0, 1);
            return items.FirstOrDefault();
        }

        public virtual T NextRecord(T current)
        {
            if (current == null || current.IsNew())
                return LastRecord();

            var next = Filter(x => x.Id > current.Id, x => x.OrderBy(x => x.Id), 0, 1).FirstOrDefault();

            if (next == null)
                return current;

            return next;
        }
        public virtual async Task<T> NextRecordAsync(T current)
        {
            if (current == null || current.IsNew())
                return LastRecord();

            var next = (await FilterAsync(x => x.Id > current.Id, x => x.OrderBy(x => x.Id), 0, 1)).FirstOrDefault();

            if (next == null)
                return current;

            return next;
        }

        public virtual T PreviousRecord(T current)
        {
            var prev = Filter(x => x.Id < current.Id, x=> x.OrderByDescending(x => x.Id), 0, 1).FirstOrDefault();

            if (prev == null)
                return current;

            return prev;
        }

        public virtual async Task<T> PreviousRecordAsync(T current)
        {
            var prev = (await FilterAsync(x => x.Id < current.Id, x => x.OrderByDescending(x => x.Id), 0, 1)).FirstOrDefault();

            if (prev == null)
                return current;

            return prev;
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
