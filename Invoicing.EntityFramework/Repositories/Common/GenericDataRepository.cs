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
        public virtual void CreateMany(IEnumerable<T> entities)
        {
            if (IsReadOnly)
            {
                _datarepositoryLogger.Error("Cannot execute CRUD operation on an readonly service.");
                throw new InvalidOperationException("Cannot execute a create operation on a readonly service.");
            }

            try
            {
                _dbContext.Set<T>().AddRange(entities);

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

        }
        public virtual async Task CreateManyAsync(IEnumerable<T> entities)
        {
            if (IsReadOnly)
            {
                _datarepositoryLogger.Error("Cannot execute CRUD operation on an readonly service.");
                throw new InvalidOperationException("Cannot execute a create operation on a readonly service.");
            }

            try
            {
                await _dbContext.Set<T>().AddRangeAsync(entities);

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

        }

        public virtual void CreateOne(T entity)
        {
            if (IsReadOnly)
            {
                _datarepositoryLogger.Error("Cannot execute CRUD operation on an readonly service.");
                throw new InvalidOperationException("Cannot execute a create operation on a readonly service.");
            }

            try
            {
                _dbContext.Set<T>().Add(entity);
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
        public virtual async Task CreateOneAsync(T entity)
        {
            if (IsReadOnly)
            {
                _datarepositoryLogger.Error("Cannot execute CRUD operation on an readonly service.");
                throw new InvalidOperationException("Cannot execute a create operation on a readonly service.");
            }

            try
            {
                await _dbContext.Set<T>().AddAsync(entity);
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
        #endregion

        #region Delete

        public virtual void DeleteMany(IEnumerable<T> entities)
        {
            if (IsReadOnly)
            {
                _datarepositoryLogger.Error("Cannot execute CRUD operation on an readonly service.");
                throw new InvalidOperationException("Cannot execute delete operation on a readonly service.");
            }

            try
            {
                _dbContext.Set<T>().RemoveRange(entities);

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
        }
        public virtual void DeleteMany(Expression<Func<T, bool>> filter)
        {
            if (IsReadOnly)
            {
                _datarepositoryLogger.Error("Cannot execute CRUD operation on an readonly service.");
                throw new InvalidOperationException("Cannot execute delete operation on a readonly service.");
            }

            var entities = Filter(filter);
            DeleteMany(entities);
        }

        public virtual async Task DeleteManyAsync(IEnumerable<T> entities)
        {
            await Task.Run(() => DeleteMany(entities));
        }
        public virtual async Task DeleteManyAsync(Expression<Func<T, bool>> filter)
        {
            await Task.Run(() => DeleteMany(filter));
        }

        public virtual void DeleteOne(T entity)
        {
            if (IsReadOnly)
            {
                _datarepositoryLogger.Error("Cannot execute CRUD operation on an readonly service.");
                throw new InvalidOperationException("Cannot execute delete operation on a readonly service.");
            }

            try
            {
                _dbContext.Remove(entity);
                _datarepositoryLogger.TraceObjectDeletion(entity);
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
        public virtual void DeleteOne(int id)
        {
            if (IsReadOnly)
            {
                _datarepositoryLogger.Error("Cannot execute CRUD operation on an readonly service.");
                throw new InvalidOperationException("Cannot execute delete operation on a readonly service.");
            }

            T entity = GetOne(id);
            DeleteOne(entity);
        }

        public virtual async Task DeleteOneAsync(T entity)
        {
            await Task.Run(() => DeleteOne(entity));
        }
        public virtual async Task DeleteOneAsync(int id)
        {
            if (IsReadOnly)
            {
                _datarepositoryLogger.Error("Cannot execute CRUD operation on an readonly service.");
                throw new InvalidOperationException("Cannot execute delete operation on a readonly service.");
            }

            T entity = await GetOneAsync(id);

            await DeleteOneAsync(entity);
        }

        #endregion

        #region Update
        public virtual void UpdateOne(T entity)
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

                _datarepositoryLogger.TraceObjectSave(orig, entity);
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
        public virtual async Task UpdateOneAsync(T entity)
        {
            await Task.Run(() => UpdateOne(entity));
        }
        #endregion

        public virtual void Save()
        {
            try
            {
                _dbContext.SaveChanges();
            } catch (Exception ex)
            {
                _datarepositoryLogger.Error(ex);
                throw;
            }
        }

        public virtual async Task SaveAsync() {
            try
            {
                await _dbContext.SaveChangesAsync();
            } catch (Exception ex)
            {
                _datarepositoryLogger.Error(ex);
                throw;
            }
        }


    }
}
