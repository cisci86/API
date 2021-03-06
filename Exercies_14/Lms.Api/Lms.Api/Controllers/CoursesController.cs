#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lms.Data.Data;
using Lms.Core.Entities;
using AutoMapper;
using Lms.Core.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Lms.Data.Data.Services;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork unitOfWork;

        public CoursesController(IMapper mapper, IUnitOfWork unitOfWork)
        {
          
            _mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoursesWithModulesDto>>> GetCourse(bool withModules)
        {
            if (!withModules)
            {
                var courses = unitOfWork.CourseRepository.GetAll();
                return Ok(_mapper.Map<IEnumerable<CoursesWithModulesDto>>(courses));
            }
            var coursesDtoMapped =  _mapper.ProjectTo<CoursesWithModulesDto>(unitOfWork.CourseRepository.GetAll());
            return Ok(await coursesDtoMapped.ToListAsync());
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            var course = await unitOfWork.CourseRepository.GetAsync(id);

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
            var courseToUpdate = await unitOfWork.CourseRepository.GetAsync(id);

            if (courseToUpdate == null)
            {
                return NotFound();
            }

            if (!TryValidateModel(course)) return BadRequest(ModelState);

            _mapper.Map(course, courseToUpdate);

            try
            {
                unitOfWork.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                return StatusCode(500);

            }

            return Ok();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCourse(int id, JsonPatchDocument<CourseModifyDto> patchDocument)
        {
            var courseToUpdate = await unitOfWork.CourseRepository.GetAsync(id);
            if (courseToUpdate == null)
            {
                return NotFound();
            }
            var courseToPatch = _mapper.Map<CourseModifyDto>(courseToUpdate);
            patchDocument.ApplyTo(courseToPatch, ModelState);

            if (!TryValidateModel(courseToPatch)) return BadRequest(ModelState);

            _mapper.Map(courseToPatch, courseToUpdate);

            try
            {
               unitOfWork.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                return StatusCode(500);

            }

            return Ok(_mapper.Map<CourseDto>(courseToUpdate));
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(CourseCreateDto courseCreateDto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var course = unitOfWork.CourseRepository.Add(_mapper.Map<Course>(courseCreateDto));
            
            try
            {
                unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {

                return StatusCode(500);
            }
            
            var courseDto = _mapper.Map<CourseDto>(course);
            return CreatedAtAction("GetCourse", new { id = course.Id }, courseDto);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await unitOfWork.CourseRepository.GetAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            unitOfWork.CourseRepository.Delete(course);
            try
            {
                unitOfWork.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                return StatusCode(500);

            }

            return NoContent();
        }
    }
}
