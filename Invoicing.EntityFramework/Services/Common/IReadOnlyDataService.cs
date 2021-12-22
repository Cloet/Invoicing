using Invoicing.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.EntityFramework.Services.Common
{
    public interface IReadOnlyDataService<T> where T : SQLModelBase<T>
    {
        public IEnumerable<T> Filter(Expression<Func<T, bool>> filter);
        public Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> filter);
        public T FirstRecord();
        public Task<T> FirstRecordAsync();
        public IEnumerable<T> GetAll();
        public Task<IEnumerable<T>> GetAllAsync();
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

    }
}