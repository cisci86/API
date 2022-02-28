# nullable disable
using System.ComponentModel.DataAnnotations;

namespace Lms.Core.Dto

{
    public class ModuleModifyDto
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
    }
}