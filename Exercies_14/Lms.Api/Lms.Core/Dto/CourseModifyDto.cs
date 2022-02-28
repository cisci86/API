using System.ComponentModel.DataAnnotations;

namespace Lms.Core.Dto
{
    public class CourseModifyDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
    }
}