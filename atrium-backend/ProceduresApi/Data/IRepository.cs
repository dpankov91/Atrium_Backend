using ProceduresApi.Models;
using System.Threading.Tasks;

namespace ProceduresApi.Data
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        Task<T> Get(int id);
        Task<T> Add(T entity);
        void Edit(T entity);
        void Remove(int id);
    }
}
