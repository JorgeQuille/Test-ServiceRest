using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Test_Calificaciones.Models
{
    public class Person
    {
        [Key]
        public int id { get; set; }
        [Required, MaxLength(10)]
        public string identification { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string firstName { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string status { get; set; }
    }
}
