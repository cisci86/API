using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Data.Services
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected LmsApiContext context;

        public GenericRepository(LmsApiContext context)
        {
            this.context = context;
        }
        public T Add(T entity)
        {
            return context.Add(entity).Entity;
        }
        public IQueryable<T> GetAll()
        {
            return context.Set<T>();
        }

        public async Task<T> GetAsync(string id)
        {
            return await context.FindAsync<T>(id);
        }
        public async Task<T> GetAsync(int id)
        {
            return await context.FindAsync<T>(id);
        }
        public virtual T Update(T entity)
        {
            return context.Update(entity)
                .Entity;
        }
        public void Delete(T id)
        {
            context.Remove(id);
        }
        public async void SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

    }
}
