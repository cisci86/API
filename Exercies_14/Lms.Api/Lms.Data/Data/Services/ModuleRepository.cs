using Lms.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Data.Services
{
    public class ModuleRepository : IRepository<Module>
    {
        public readonly LmsApiContext _context;
        public ModuleRepository(LmsApiContext context)
        {
            _context = context;
        }

        public void Add(Module module)
        {
           _context.Module.Add(module);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
