using System.ComponentModel.DataAnnotations;

namespace Lms.Core.Dto
{
    public class CourseDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        private DateTime endDate;

        public DateTime EndDate
        {
            get { return StartDate.AddMonths(3); }
            set { endDate = value; }
        }

    }
}
