﻿#nullable disable
using AutoMapper;
using Lms.Core.Dto;
using Lms.Core.Entities;
using Lms.Data.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet("{title}")]
        public async Task<ActionResult<ModuleDto>> GetModule(string title)
        {
            var module = await _context.Module.FirstOrDefaultAsync(m => m.Title == title);

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

            _mapper.Map(module, moduleToUpdate);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500);

            }


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
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException ex)
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

            _context.Module.Add(module);

            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException ex)
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
            var @module = await _context.Module.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }

            _context.Module.Remove(@module);
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500);

            }
            return Ok();
        }
    }
}
