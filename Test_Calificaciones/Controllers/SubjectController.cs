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
    public class SubjectController : ControllerBase
    {
        private readonly ApplicationDbContext _data;

        public SubjectController(ApplicationDbContext db)
        {
            _data = db;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Subject>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetSubjects()
        {
            var subjects = await _data.subjects.OrderBy(p => p.name).Include(p=> p.teacher).ToListAsync();
            return Ok(subjects);
        }

        [HttpGet("{id:int}", Name = "GetSubject")]
        [ProducesResponseType(200, Type = typeof(Subject))]
        [ProducesResponseType(400)] //bad request
        [ProducesResponseType(404)] //not found
        public async Task<IActionResult> GetSubject(int id)
        {
            var subject = await _data.subjects.FirstOrDefaultAsync(s => s.id == id);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)] //internal error
        public async Task<IActionResult> CreateSubject([FromBody] Subject subject)
        {
            if (subject == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _data.AddAsync(subject);
            await _data.SaveChangesAsync();
            return CreatedAtRoute("GetSubject", new { id = subject.id }, subject);
        }
    }
}

