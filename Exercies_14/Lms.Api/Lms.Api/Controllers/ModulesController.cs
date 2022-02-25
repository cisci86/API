#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class ModulesController : ControllerBase
    {
        private readonly LmsApiContext _context;
        private readonly IMapper _mapper;


        public ModulesController(LmsApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetModule()
        {
            var modelDto = _mapper.ProjectTo<ModuleDto>(_context.Module);
            return await modelDto.ToListAsync();
        }

        // GET: api/Modules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModuleDto>> GetModule(int id)
        {
            var module = await _context.Module.FindAsync(id);

            if (module == null)
            {
                return NotFound();
            }

            return _mapper.Map<ModuleDto>(module);
        }

        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(int id, ModuleModifyDto module)
        {
            var moduleToUpdate = await _context.Module.FindAsync(id);
            
            if (moduleToUpdate == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                BadRequest();
            }
            _mapper.Map(module, moduleToUpdate);

            _context.SaveChanges();


            return Ok();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchModule(int id, JsonPatchDocument<ModuleModifyDto> patchDocument)
        {
            var moduleToUpdate = await _context.Module.FindAsync(id);
            if (moduleToUpdate == null)
            {
                return NotFound();
            }
            var moduleToPatch = _mapper.Map<ModuleModifyDto>(moduleToUpdate);
            patchDocument.ApplyTo(moduleToPatch);
            _mapper.Map(moduleToPatch, moduleToUpdate);
            _context.SaveChanges();

            return NoContent();
        }
        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Module>> PostModule(ModuleCreateDto moduleCreateDto)
        {
            var module = _mapper.Map<Module>(moduleCreateDto);
            _context.Module.Add(module);
            await _context.SaveChangesAsync();
            var moduleDto = _mapper.Map<ModuleDto>(module);

            return CreatedAtAction("GetModule", new { id = module.Id }, moduleDto);
        }

        // DELETE: api/Modules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            var @module = await _context.Module.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }

            _context.Module.Remove(@module);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModuleExists(int id)
        {
            return _context.Module.Any(e => e.Id == id);
        }
    }
}
