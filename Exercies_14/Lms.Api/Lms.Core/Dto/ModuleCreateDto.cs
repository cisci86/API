using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Dto
{
    public class ModuleCreateDto
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        [Required]
        public int CourseId { get; set; }
    }
}
