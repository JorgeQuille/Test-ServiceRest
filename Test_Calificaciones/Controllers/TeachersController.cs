using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Calificaciones.Data;
using Test_Calificaciones.Models;

namespace Test_Calificaciones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeachersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Teacher>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult> GetTeachers()
        {
            var teachers = await _context.teachers.OrderBy(p => p.lastName).ToListAsync();
            return Ok(teachers);
        }


        [HttpGet("{id:int}", Name = "GetTeacher")]
        [ProducesResponseType(200, Type = typeof(Teacher))]
        [ProducesResponseType(400)] //bad request
        [ProducesResponseType(404)] //not found
        public async Task<ActionResult> GetTeacher(int id)
        {
            var teacher = await _context.teachers.FirstOrDefaultAsync(s => s.id == id);

            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)] //internal error
        public async Task<IActionResult> CreateTeacher([FromBody] Teacher teacher)
        {
            if (teacher == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _context.AddAsync(teacher);
            await _context.SaveChangesAsync();
            return CreatedAtRoute("GetTeacher", new { id = teacher.id }, teacher);
        }
    
    /*
        // POST: api/Teachers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
            _context.teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeacher", new { id = teacher.id }, teacher);
        }

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeacherExists(int id)
        {
            return _context.teachers.Any(e => e.id == id);
        }*/
    }
}
