using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
# nullable disable
namespace Lms.Core.Entities
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public ICollection<Module> Modules { get; set; }
    }
}
