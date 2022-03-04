
namespace Lms.Data.Data.Services
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);
        void Delete(T id);
        IQueryable<T> GetAll();
        Task<T> GetAsync(int id);
        Task<T> GetAsync(string id);
        void SaveChangesAsync();
        T Update(T entity);
    }
}