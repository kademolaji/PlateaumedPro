using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateaumedPro.Domain
{
    public class Teacher : BaseEntity<long>
    {
        [Required]
        public string NationalIDNumber { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string TeacherNumber { get; set; }
        public decimal? Salary { get; set; }
    }
}
