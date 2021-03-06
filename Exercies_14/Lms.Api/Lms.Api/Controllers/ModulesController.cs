#nullable disable
using AutoMapper;
using Lms.Core.Dto;
using Lms.Core.Entities;
using Lms.Data.Data;
using Lms.Data.Data.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork unitOfWork;

        public ModulesController(IMapper mapper, IUnitOfWork unitOfWork)
        {

            _mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetModule()
        {
            var modelDto = _mapper.ProjectTo<ModuleDto>(unitOfWork.ModuleRepository.GetAll());
            return await modelDto.ToListAsync();
        }

        // GET: api/Modules/5
        [HttpGet("{title}")]
        public async Task<ActionResult<ModuleDto>> GetModule(string title)
        {
            var module = await _mapper.ProjectTo<ModuleDto>(unitOfWork.ModuleRepository.GetAll())
                                .FirstOrDefaultAsync(m => m.Title == title);
            if (module == null)
            {
                return NotFound();
            }
            return Ok(module);
        }

        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(int id, ModuleModifyDto module)
        {
            var moduleToUpdate = await unitOfWork.ModuleRepository.GetAsync(id);

            if (moduleToUpdate == null)
            {
                return NotFound();
            }
            if (!TryValidateModel(module)) return BadRequest(ModelState);

            _mapper.Map(module, moduleToUpdate);

            try
            {
                await unitOfWork.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                return StatusCode(500);

            }
             return Ok();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchModule(int id, JsonPatchDocument<ModuleModifyDto> patchDocument)
        {
            var moduleToUpdate = await unitOfWork.ModuleRepository.GetAsync(id);
            if (moduleToUpdate == null)
            {
                return NotFound();
            }
            var moduleToPatch = _mapper.Map<ModuleModifyDto>(moduleToUpdate);
            patchDocument.ApplyTo(moduleToPatch);

            if (!TryValidateModel(moduleToPatch)) return BadRequest(ModelState);

            _mapper.Map(moduleToPatch, moduleToUpdate);
            
            try
            {
                await unitOfWork.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                return StatusCode(500);

            }

            return Ok();
        }
        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Module>> PostModule(ModuleCreateDto moduleCreateDto)
        {
            var module = _mapper.Map<Module>(moduleCreateDto);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            unitOfWork.ModuleRepository.Add(module);

            try
            {
               await unitOfWork.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                return StatusCode(500);

            }

            var moduleDto = _mapper.Map<ModuleDto>(module);

            return CreatedAtAction("GetModule", new { id = module.Id }, moduleDto);
        }

        // DELETE: api/Modules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            var module = await unitOfWork.ModuleRepository.GetAsync(id);
            if (module == null)
            {
                return NotFound();
            }

            unitOfWork.ModuleRepository.Delete(module);
            try
            {
                await unitOfWork.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                return StatusCode(500);

            }
            return Ok();
        }
    }
}
