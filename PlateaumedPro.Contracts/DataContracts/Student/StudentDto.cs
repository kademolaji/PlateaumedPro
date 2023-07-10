using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateaumedPro.Contracts
{
    public class StudentDto
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string NationalIDNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string StudentNumber { get; set; }
    }
}