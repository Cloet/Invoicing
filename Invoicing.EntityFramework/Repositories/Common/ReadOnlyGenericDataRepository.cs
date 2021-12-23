using Invoicing.Domain.Model.Common;
using Invoicing.Base.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework.Repositories.Common
{
    public abstract class ReadOnlyGenericDataRepository<T, TContext> : IReadOnlyDataRepository<T> where T : SQLModelBase<T> where TContext : SQLDbContext
    {

        protected readonly ICLogger _datarepositoryLogger = null;
        protected readonly TContext _dbContext;

        protected DataRepoOptions _options { get; set; } = null;
        public bool AsNoTracking => _options.AsNoTracking;

        public ReadOnlyGenericDataRepository(IDbContextFactory<TContext> contextFactory, DataRepoOptions options = null)
        {
            if (options == null)
            {
                options = new DataRepoOptions()
                {
                    AsNoTracking = true,
                    IsReadOnly = false
                };
            }

            _dbContext = contextFactory.CreateDBContext();
            _datarepositoryLogger = LogManager.GetLogger(GetType());
            _options = options;
        }

        protected virtual IQueryable<T> QueryIncludes(IQueryable<T> query)
        {
            return query;
        }

        protected IQueryable<T> QueryModifiers(IQueryable<T> query)
        {
            query = QueryIncludes(query)
                .OrderBy(x => x.Id);

            if (AsNoTracking)
                query = query.AsNoTracking();

            return query;
        }

        #region Filter
        public virtual async Task<IQueryable<T>> FilterAsync(Expression<Func<T, bool>> filter, int results = -1)
        {
            return await Task.Run(() => Filter(filter, results));
        }

        public virtual IQueryable<T> Filter(Expression<Func<T, bool>> filter, int results = -1)
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
                _datarepositoryLogger.Error(ex);
                throw;
            }
        }
        #endregion

        #region Get All
        public virtual IQueryable<T> GetAll()
        {
            try
            {
                return QueryModifiers(_dbContext.Set<T>());
            }
            catch (Exception ex)
            {
                _datarepositoryLogger.Error(ex);
                throw;
            }
        }
        public virtual async Task<IQueryable<T>> GetAllAsync()
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
                _datarepositoryLogger.Error(ex);
                throw;
            }
        }
        public virtual async Task<T> GetOneAsync(int id)
        {
            return await Task.Run(() => GetOne(id));
        }
        #endregion

    }
}
