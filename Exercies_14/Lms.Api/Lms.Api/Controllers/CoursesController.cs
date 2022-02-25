#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lms.Data.Data;
using Lms.Core.Entities;
using AutoMapper;
using Lms.Core.Dto;
using Microsoft.AspNetCore.JsonPatch;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly LmsApiContext _context;
        private readonly IMapper _mapper;

        public CoursesController(LmsApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourse()
        {
            var coursesDto = _mapper.ProjectTo<CourseDto>(_context.Course);
            return await coursesDto.ToListAsync();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return _mapper.Map<CourseDto>(course);
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, CourseModifyDto course)
        {
            var courseToUpdate = await _context.Course.FindAsync(id);
            if (courseToUpdate == null)
            {
                return NotFound();
            }
            _mapper.Map(course, courseToUpdate);
            
            _context.SaveChanges();
           

            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCourse(int id, JsonPatchDocument<CourseModifyDto> patchDocument)
        {
            var courseToUpdate = await _context.Course.FindAsync(id);
            if (courseToUpdate == null)
            {
                return NotFound();
            }
            var courseToPatch = _mapper.Map<CourseModifyDto>(courseToUpdate);
            patchDocument.ApplyTo(courseToPatch);
            _mapper.Map(courseToPatch, courseToUpdate);
            _context.SaveChanges();
           
            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(CourseCreateDto courseCreateDto)
        {
            var course = _mapper.Map<Course>(courseCreateDto);
            _context.Course.Add(course);
            await _context.SaveChangesAsync();
            var courseDto = _mapper.Map<CourseDto>(course);
            return CreatedAtAction("GetCourse", new { id = course.Id }, courseDto);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Course.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.Id == id);
        }
    }
}
