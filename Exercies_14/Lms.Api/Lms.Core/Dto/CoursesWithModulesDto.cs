using Lms.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Dto
{
    public class CoursesWithModulesDto
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        private DateTime endDate;

        public DateTime EndDate
        {
            get { return StartDate.AddMonths(3); }
            set { endDate = value; }
        }
        public ICollection<ModuleDto> Modules { get; set; }
    }
}
