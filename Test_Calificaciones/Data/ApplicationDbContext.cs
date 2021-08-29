using Microsoft.EntityFrameworkCore;
using Test_Calificaciones.Models;

namespace Test_Calificaciones.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {

        }

        public DbSet<Teacher> teachers { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Subject> subjects { get; set; }
        public DbSet<StudentSubjectGrade> studentSubjectGrades { get; set; }

    }
}
