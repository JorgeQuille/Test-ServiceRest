using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_Calificaciones.Models
{
    public class Subject
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int  teacherId { get; set; }
        [ForeignKey("teacherId")]
        public Teacher teacher { get; set; }

    }
}
