﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Dto
{
    public class CourseCreateDto
    {
        [MaxLength(100)]
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
    }
}
