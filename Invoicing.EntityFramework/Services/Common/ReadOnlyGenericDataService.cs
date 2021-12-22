using Invoicing.Domain.Model.Common;
using Invoicing.Base.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework.Services.Common
{
    public abstract class ReadOnlyGenericDataService<T, TContext> : IReadOnlyDataService<T> where T : SQLModelBase<T> where TContext : SQLDbContext
    {

        protected readonly ICLogger _dataserviceLogger = null;
        protected readonly TContext _dbContext;

        protected DataServiceOptions _options { get; set; } = null;
        public bool AsNoTracking => _options.AsNoTracking;

        public ReadOnlyGenericDataService(IDbContextFactory<TContext> contextFactory, DataServiceOptions options = null)
        {
            if (options == null)
            {
                options = new DataServiceOptions()
                {
                    AsNoTracking = true,
                    IsReadOnly = false
                };
            }

            _dbContext = contextFactory.CreateDBContext();
            _dataserviceLogger = LogManager.GetLogger(GetType());
            _options = options;
        }

        protected virtual IQueryable<T> QueryIncludes(IQueryable<T> query)
        {
            return query;
        }

        protected IQueryable<T> QueryModifiers(IQueryable<T> query)
        {
            query = QueryIncludes(query);

            if (AsNoTracking)
                query = query.AsNoTracking();

            return query;
        }

        #region Filter
        public virtual IEnumerable<T> Filter(Expression<Func<T, bool>> filter)
        {
            return Filter(filter, -1);
        }
        public virtual async Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> filter)
        {
            return await FilterAsync(filter, -1);
        }
        public virtual async Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> filter, int results = -1)
        {
            return await Task.Run(() => Filter(filter, results));
        }

        public virtual IEnumerable<T> Filter(Expression<Func<T,bool>> filter, int results = -1)
        {
            try
            {
                var set = _dbContext.Set<T>();

                if (results > 0)
                    return QueryModifiers(set.Where(filter)).Take(results);

                return QueryModifiers(set.Where(filter));
            }
            catch (Exception ex)
            {
                _dataserviceLogger.Error(ex);
                throw;
            }
        }
        #endregion

        #region First Record
        public virtual T FirstRecord()
        {
            try
            {
                return QueryModifiers(_dbContext.Set<T>())
                    .OrderBy(x => x.Id)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _dataserviceLogger.Error(ex);
                throw;
            }
        }
        public virtual async Task<T> FirstRecordAsync()
        {
            return await Task.Run(() => FirstRecord());
        }
        #endregion

        #region Get All
        public virtual IEnumerable<T> GetAll()
        {
            try
            {
                return QueryModifiers(_dbContext.Set<T>());
            }
            catch (Exception ex)
            {
                _dataserviceLogger.Error(ex);
                throw;
            }
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.Run(() => GetAll());
        }
        #endregion

        #region GetOne
        public virtual T GetOne(int id)
        {
            try
            {
                return QueryModifiers(_dbContext.Set<T>())
                    .FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _dataserviceLogger.Error(ex);
                throw;
            }
        }
        public virtual async Task<T> GetOneAsync(int id)
        {
            return await Task.Run(() => GetOne(id));
        }
        #endregion

        #region Last Record
        public T LastRecord()
        {
            try
            {
                return QueryModifiers(_dbContext.Set<T>())
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _dataserviceLogger.Error(ex);
                throw;
            }
        }
        public virtual async Task<T> LastRecordAsync()
        {
            return await Task.Run(() => LastRecord());
        }
        #endregion

        #region Next Record
        public T NextRecord(T current)
        {
            if (current == null || current.IsNew())
                return LastRecord();

            try
            {
                var next = QueryModifiers(_dbContext.Set<T>())
                    .Where(x => x.Id > current.Id)
                    .OrderBy(x => x.Id)
                    .FirstOrDefault();

                if (next == null)
                    return current;

                return next;
            }
            catch (Exception ex)
            {
                _dataserviceLogger.Error(ex);
                throw;
            }

        }
        public virtual async Task<T> NextRecordAsync(T current)
        {
            return await Task.Run(() => NextRecord(current));
        }
        #endregion

        #region Previous record
        public virtual T PreviousRecord(T current)
        {
            try
            {
                var prev = QueryModifiers(_dbContext.Set<T>())
                    .Where(x => x.Id < current.Id)
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefault();

                if (prev == null)
                    return current;

                return prev;
            }
            catch (Exception ex)
            {
                _dataserviceLogger.Error(ex);
                throw;
            }
        }
        public virtual async Task<T> PreviousRecordAsync(T current)
        {
            return await Task.Run(() => PreviousRecord(current));
        }
        #endregion

        #region Record exists
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
        #endregion
    }
}
