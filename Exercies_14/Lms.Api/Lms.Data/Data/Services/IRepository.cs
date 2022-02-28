using Lms.Core.Entities;

namespace Lms.Data.Data.Services
{
    public interface IRepository<T>
    {
        void Add(T item);
        bool Save();
    }
}