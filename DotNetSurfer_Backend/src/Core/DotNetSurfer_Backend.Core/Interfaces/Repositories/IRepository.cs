using System.Threading.Tasks;

namespace DotNetSurfer_Backend.Core.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveAsync();
    }
}
