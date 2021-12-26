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
        protected bool _disposed = false;

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

        protected virtual IQueryable<T> QueryModifiers(IQueryable<T> query)
        {
            if (AsNoTracking)
                query = query.AsNoTracking();

            return query;
        }
       
        #region Filter
        public virtual async Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int take = -1, int skip = -1)
        {
            return await Task.Run(() => Filter(filter, orderBy, skip, take));
        }

        public virtual IEnumerable<T> Filter(Expression<Func<T,bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int take = -1, int skip = -1)
        {
            try
            {
                var query = QueryIncludes(_dbContext.Set<T>());
                query = QueryModifiers(query);

                if (filter != null)
                    query = query.Where(filter);
                if (orderBy != null)
                    query = orderBy(query);
                if (skip > 0)
                    query = query.Skip(skip);
                if (take > 0)
                    query = query.Take(take);

                return query;
            } catch (Exception ex)
            {
                _datarepositoryLogger.Error(ex);
                throw;
            }
        }


        #endregion

        #region Get All
        public virtual IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            try
            {
                var query = QueryModifiers(QueryIncludes(_dbContext.Set<T>()));
                if (orderBy != null)
                    query = orderBy(query);
                return query;
            }
            catch (Exception ex)
            {
                _datarepositoryLogger.Error(ex);
                throw;
            }
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            return await Task.Run(() => GetAll(orderBy));
        }
        #endregion

        #region GetOne
        public virtual T GetOne(int id)
        {
            try
            {
                return QueryModifiers(QueryIncludes(_dbContext.Set<T>()))
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


        protected virtual void Dispose(bool disposing) {
            if (_disposed)
                return;

            if (disposing)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
