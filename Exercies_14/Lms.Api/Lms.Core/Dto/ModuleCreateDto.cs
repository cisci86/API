﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Dto
{
    public class ModuleCreateDto
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public int CourseId { get; set; }
    }
}
