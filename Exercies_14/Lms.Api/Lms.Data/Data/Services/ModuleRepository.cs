using Lms.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Data.Services
{
    public class ModuleRepository : GenericRepository<Module>
    {
       
        public ModuleRepository(LmsApiContext context) : base(context)
        {
            
        }
    }
}
