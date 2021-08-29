using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Calificaciones.Models
{
    public class StudentSubjectGrade
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int subjectId { get; set; }
        [ForeignKey("subjectId")]
        public Subject subject { get; set; }
        [Required]
        public int studentId { get; set; }
        [ForeignKey("studentId")]
        public Student student { get; set; }
        [Range(0,10, ErrorMessage = "La Calificación debe tener un rango de 0 - 10")]
        public double homework_grade { get; set; }
        [Range(0, 10, ErrorMessage = "La Calificación debe tener un rango de 0 - 10")]
        public double test_grade { get; set; }
        public double average { get; set; }
        public string status_Subject { get; set; }

    }
}
