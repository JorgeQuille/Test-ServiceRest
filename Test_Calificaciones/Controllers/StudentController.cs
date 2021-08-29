using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Calificaciones.Data;
using Test_Calificaciones.Models;

namespace Test_Calificaciones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _data;

        public StudentController(ApplicationDbContext db)
        {
            _data = db;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Student>))]
        [ProducesResponseType (400)]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _data.students.OrderBy(p => p.lastName).ToListAsync();
            return Ok(students);
        }

        [HttpGet("{id:int}", Name = "GetStudent")]
        [ProducesResponseType(200, Type = typeof(Student))]
        [ProducesResponseType(400)] //bad request
        [ProducesResponseType(404)] //not found
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _data.students.FirstOrDefaultAsync(s => s.id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)] //internal error
        public async Task<IActionResult> CreateStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _data.AddAsync(student);
            await _data.SaveChangesAsync();
            return CreatedAtRoute("GetStudent", new { id = student.id},student);
        }
    }
}
