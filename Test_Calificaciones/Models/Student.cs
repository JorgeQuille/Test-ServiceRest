using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_Calificaciones.Models
{
    public class Student : Person
    {
        public Student() : base()
        {

        }
        [Required]
        public string ciclo { get; set; }
        [Required]
        public string parallel { get; set; }
    }
}
