using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Dto
{
    internal class ModuleDto
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        private DateTime endDate;

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = StartDate.AddMonths(1); }
        }

    }
}
