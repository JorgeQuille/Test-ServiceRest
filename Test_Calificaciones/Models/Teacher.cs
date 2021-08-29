using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test_Calificaciones.Models
{
    public class Teacher : Person
    {
        public Teacher() : base()
        {

        }
        [Required]
        public string tituloAcademico { get; set; }
        //public List<Subject> subjects { get; set; }
    }
}
