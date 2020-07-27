using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CashewDocumentParser.Models.Infrastructure
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<List<T>> GetAll();
        List<T> GetMultiple(Func<T, bool> conditions);
        T GetSingle(Func<T, bool> conditions);
        Task<T> Get(int id);
        T Add(T entity);
        T Update(T entity);
        Task<T> Delete(int id);
    }
}
