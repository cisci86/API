using Lms.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Data.Services
{
    public interface IUnitOfWork
    {
        IRepository<Course> CourseRepository { get; }
        IRepository<Module> ModuleRepository { get; }

        Task<bool> SaveChangesAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly LmsApiContext context;

        public UnitOfWork(LmsApiContext context)
        {
            this.context = context;
        }
        private IRepository<Course> courseRepository;
        public IRepository<Course> CourseRepository
        {
            get
            {
                if (courseRepository == null)
                {
                    courseRepository = new CourseRepository(context);
                }
                return courseRepository;
            }
        }
        private IRepository<Module> moduleRepository;
        public IRepository<Module> ModuleRepository
        {
            get
            {
                if (moduleRepository == null)
                {
                    moduleRepository = new ModuleRepository(context);
                }
                return moduleRepository;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await context.SaveChangesAsync()) >= 0;
        }
    }
}
