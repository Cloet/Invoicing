using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.Domain.Services.Common
{
    public interface IReadOnlyDataService<T>
    {
        Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> filter);
        IEnumerable<T> Filter(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetAll();

        Task<T> GetOneAsync(int id);
        T GetOne(int id);

        Task<T> FirstRecordAsync();
        T FirstRecord();

        Task<T> LastRecordAsync();
        T LastRecord();

        Task<T> NextRecordAsync(T current);
        T NextRecord(T current);

        Task<T> PreviousRecordAsync(T current);
        T PreviousRecord(T current);

        Task<bool> RecordExistsAsync(int id);
        bool RecordExists(int id);

    }
}
