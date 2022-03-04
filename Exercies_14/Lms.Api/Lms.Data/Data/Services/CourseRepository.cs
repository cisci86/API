using Lms.Core.Entities;

namespace Lms.Data.Data.Services
{
    public class CourseRepository : GenericRepository<Course>
    {


        public CourseRepository(LmsApiContext context) : base(context)
        {

        }
    }
}
