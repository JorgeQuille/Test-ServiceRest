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
    public class StudentSubjectGradeController : ControllerBase
    {
        private readonly ApplicationDbContext _data;

        public StudentSubjectGradeController(ApplicationDbContext db)
        {
            _data = db;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<StudentSubjectGrade>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetStudentSubjectGrades()
        {
            var studentsSubjectGrade = await _data.studentSubjectGrades.OrderBy(p => p.id).Include(p =>p.student)
                .Include(p=>p.subject).Include(p => p.subject.teacher).ToListAsync();
            return Ok(studentsSubjectGrade);
        }

        [HttpGet("{id:int}", Name = "GetStudentSubjectGrade")]
        [ProducesResponseType(200, Type = typeof(Student))]
        [ProducesResponseType(400)] //bad request
        [ProducesResponseType(404)] //not found
        public async Task<IActionResult> GetStudentSubjectGrade(int id)
        {
            var studentSubjectGrade = await _data.studentSubjectGrades.Include(p => p.student).Include(p => p.subject).FirstOrDefaultAsync(s => s.id == id);
            if (studentSubjectGrade == null)
            {
                return NotFound();
            }
            return Ok(studentSubjectGrade);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)] //internal error
        public async Task<IActionResult> CreateStudentSubjectGrade([FromBody] StudentSubjectGrade grade)
        {
            if (grade == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            grade.average = (grade.homework_grade + grade.test_grade) / 2;
            if(grade.average >= 7)
            {
                grade.status_Subject = "APROBADA";
            }
            else
            {
                grade.status_Subject = "REPROBADA";
            }
            await _data.AddAsync(grade);
            await _data.SaveChangesAsync();
            return CreatedAtRoute("GetStudentSubjectGrade", new { id = grade.id }, grade);
        }
    }
}

