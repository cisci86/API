using Lms.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lms.Data.Data.Services
{
    public class CourseRepository : IRepository<Course>
    {
        private readonly LmsApiContext _context;

        public CourseRepository(LmsApiContext context)
        {
            _context = context;
        }
        public void Add(Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }
            _context.Course.Add(course);
        }
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
