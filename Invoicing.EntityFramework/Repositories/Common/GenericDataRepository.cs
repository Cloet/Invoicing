using Invoicing.Domain.Model.Common;
using Invoicing.Base.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework.Repositories.Common
{
    public abstract class GenericDataRepository<T, TContext> : ReadOnlyGenericDataRepository<T, TContext>, IGenericDataRepository<T> where T : SQLModelBase<T> where TContext : SQLDbContext
    {

        public bool IsReadOnly => _options.IsReadOnly;

        public GenericDataRepository(IDbContextFactory<TContext> contextFactory, DataRepoOptions options = null) : base(contextFactory, options)
        {
        }

        public virtual TEntity Detach<TEntity>(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        #region Create
        public virtual bool CreateMany(IEnumerable<T> entities)
        {
            if (IsReadOnly)
            {
                _datarepositoryLogger.Error("Cannot execute CRUD operation on an readonly service.");
                throw new InvalidOperationException("Cannot execute a create operation on a readonly service.");
            }

            try
            {
                _dbContext.Set<T>().AddRange(entities);
                _dbContext.SaveChanges();

                // Log created objects.
                if (_datarepositoryLogger.LoggerLevel >= LogLevel.Trace)
                {
                    foreach (var e in entities)
                        _datarepositoryLogger.TraceObjectCreation(e);
                }

            }
            catch (DbUpdateException ex)
            {
                _datarepositoryLogger.Error(ex);
                throw new InvalidOperationException(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                _datarepositoryLogger.Error(ex);
                throw;
            }

            return true;
        }
        public virtual async Task<bool> CreateManyAsync(IEnumerable<T> entities)
        {
            if (IsReadOnly)
            {
                _datarepositoryLogger.Error("Cannot execute CRUD operation on an readonly service.");
                throw new InvalidOperationException("Cannot execute a create operation on a readonly service.");
            }

            try
            {
                await _dbContext.Set<T>().AddRangeAsync(entities);
                await _dbContext.SaveChangesAsync();

                // Log created objects.
                if (_datarepositoryLogger.LoggerLevel >= LogLevel.Trace)
                {
                    foreach (var e in entities)
                        _datarepositoryLogger.TraceObjectCreation(e);
                }

                return true;
            }
            catch (DbUpdateException ex)
            {
                _datarepositoryLogger.Error(ex);
                throw new InvalidOperationException(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                _datarepositoryLogger.Error(ex);
                throw;
            }

        }

        public virtual T CreateOne(T entity)
        {
            T created = null;

            if (IsReadOnly)
            {
                _datarepositoryLogger.Error("Cannot execute CRUD operation on an readonly service.");
                throw new InvalidOperationException("Cannot execute a create operation on a readonly service.");
            }

            try
            {
                EntityEntry<T> createdResult = _dbContext.Set<T>().Add(entity);
                _dbContext.SaveChanges();

                created = GetOne(createdResult.Entity.Id);
            }
            catch (DbUpdateException ex)
            {
                _datarepositoryLogger.Error(ex);
                throw new InvalidOperationException(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                _datarepositoryLogger.Error(ex);
                throw;
            }

            return created;
        }
        public virtual async Task<T> CreateOneAsync(T entity)
        {
            T created = null;

            if (IsReadOnly)
            {
                _datarepositoryLogger.Error("Cannot execute CRUD operation on an readonly service.");
                throw new InvalidOperationException("Cannot execute a create operation on a readonly service.");
            }

            try
            {
                EntityEntry<T> createdResult = await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                created = GetOne(createdResult.Entity.Id);
            }
            catch (DbUpdateException ex)
            {
                _datarepositoryLogger.Error(ex);
                throw new InvalidOperationException(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                _datarepositoryLogger.Error(ex);
                throw;
            }

            return created;
        }
        #endregion

        #region Delete

        public virtual bool DeleteMany(IEnumerable<T> entities)
        {
            bool deleted = false;

            if (IsReadOnly)
            {
                _datarepositoryLogger.Error("Cannot execute CRUD operation on an readonly service.");
                throw new InvalidOperationException("Cannot execute delete operation on a readonly service.");
            }

            try
            {
                _dbContext.Set<T>().RemoveRange(entities);
                _dbContext.SaveChanges();

                if (_datarepositoryLogger.LoggerLevel >= LogLevel.Trace)
                {
                    foreach (var e in entities)
                        _datarepositoryLogger.TraceObjectDeletion(e);
                }

            }
            catch (DbUpdateException ex)
            {
                _datarepositoryLogger.Error(ex);
                throw new InvalidOperationException(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                _datarepositoryLogger.Error(ex);
                throw;
            }

            return deleted;
        }
        public virtual bool DeleteMany(Expression<Func<T, bool>> filter)
        {
            if (IsReadOnly)
            {
                _datarepositoryLogger.Error("Cannot execute CRUD operation on an readonly service.");
                throw new InvalidOperationException("Cannot execute delete operation on a readonly service.");
            }

            var entities = _dbContext.Set<T>().Where(filter);
            return DeleteMany(entities);
        }

        public virtual async Task<bool> DeleteManyAsync(IEnumerable<T> entities)
        {
            bool deleted = false;

            if (IsReadOnly)
            {
                _datarepositoryLogger.Error("Cannot execute CRUD operation on an readonly service.");
                throw new InvalidOperationException("Cannot execute delete operation on a readonly service.");
            }

            try
            {
                _dbContext.Set<T>().RemoveRange(entities);
                await _dbContext.SaveChangesAsync();

                if (_datarepositoryLogger.LoggerLevel >= LogLevel.Trace)
                {
                    foreach (var e in entities)
                        _datarepositoryLogger.TraceObjectDeletion(e);
                }

            }
            catch (DbUpdateException ex)
            {
                _datarepositoryLogger.Error(ex);
                throw new InvalidOperationException(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                _datarepositoryLogger.Error(ex);
                throw;
            }

            return deleted;
        }
        public virtual async Task<bool> DeleteManyAsync(Expression<Func<T, bool>> filter)
        {
            if (IsReadOnly)
            {
                _datarepositoryLogger.Error("Cannot execute CRUD operation on an readonly service.");
                throw new InvalidOperationException("Cannot execute delete operation on a readonly service.");
            }

            var entities = _dbContext.Set<T>().Where(filter);
            return await DeleteManyAsync(entities);
        }

        public virtual bool DeleteOne(T entity)
        {
            var deleted = false;

            if (IsReadOnly)
            {
                _datarepositoryLogger.Error("Cannot execute CRUD operation on an readonly service.");
                throw new InvalidOperationException("Cannot execute delete operation on a readonly service.");
            }

            try
            {
                _dbContext.Remove(entity);
                _dbContext.SaveChanges();

                _datarepositoryLogger.TraceObjectDeletion(entity);

                deleted = true;
            }
            catch (DbUpdateException ex)
            {
                _datarepositoryLogger.Error(ex);
                throw new InvalidOperationException(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                _datarepositoryLogger.Error(ex);
                throw;
            }

            return deleted;
        }
        public virtual bool DeleteOne(int id)
        {
            if (IsReadOnly)
            {
                _datarepositoryLogger.Error("Cannot execute CRUD operation on an readonly service.");
                throw new InvalidOperationException("Cannot execute delete operation on a readonly service.");
            }

            T entity = _dbContext.Set<T>().FirstOrDefault(x => x.Id == id);
            return DeleteOne(entity);
        }

        public virtual async Task<bool> DeleteOneAsync(T entity)
        {
            var deleted = false;

            if (IsReadOnly)
            {
                _datarepositoryLogger.Error("Cannot execute CRUD operation on an readonly service.");
                throw new InvalidOperationException("Cannot execute delete operation on a readonly service.");
            }

            try
            {
                _dbContext.Remove(entity);
                await _dbContext.SaveChangesAsync();

                _datarepositoryLogger.TraceObjectDeletion(entity);

                deleted = true;
            }
            catch (DbUpdateException ex)
            {
                _datarepositoryLogger.Error(ex);
                throw new InvalidOperationException(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                _datarepositoryLogger.Error(ex);
                throw;
            }

            return deleted;
        }
        public virtual async Task<bool> DeleteOneAsync(int id)
        {
            if (IsReadOnly)
            {
                _datarepositoryLogger.Error("Cannot execute CRUD operation on an readonly service.");
                throw new InvalidOperationException("Cannot execute delete operation on a readonly service.");
            }

            T entity = await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

            return await DeleteOneAsync(entity);
        }

        #endregion

        #region Update
        public virtual T UpdateOne(T entity)
        {
            if (IsReadOnly)
            {
                _datarepositoryLogger.Error("Cannot execute CRUD operation on an readonly service.");
                throw new InvalidOperationException("Cannot execute an update operation on a readonly service.");
            }

            try
            {
                if (entity.Id <= 0)
                    throw new ArgumentException(nameof(entity));

                var orig = GetOne(entity.Id);
                _dbContext.Set<T>().Update(entity);
                _dbContext.SaveChanges();

                _datarepositoryLogger.TraceObjectSave(orig, entity);

                // Nieuwe object uit db halen.
                return GetOne(entity.Id);
            }
            catch (DbUpdateException ex)
            {
                _datarepositoryLogger.Error(ex);
                throw new InvalidOperationException(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                _datarepositoryLogger.Error(ex);
                throw;
            }
        }
        public virtual async Task<T> UpdateOneAsync(T entity)
        {
            return await Task.Run(() => UpdateOne(entity));
        }
        #endregion
    }
}
