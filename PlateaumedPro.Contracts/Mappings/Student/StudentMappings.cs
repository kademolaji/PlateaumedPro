using PlateaumedPro.Contracts;
using PlateaumedPro.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PlateaumedPro.Contracts
{
    public static class StudentMappings
    {
        public static T ToModel<T>(this Student entity) where T : StudentDto, new()
        {
            return new T
            {
                Id = entity.Id,
                NationalIDNumber = entity.NationalIDNumber,
                Name = entity.Name,
                Surname = entity.Surname,
                DateOfBirth = entity.DateOfBirth,
                StudentNumber = entity.StudentNumber,

            };

        }

        public static T ToModel<T>(this StudentDto entity) where T : Student, new()
        {
            return new T
            {
                Id = entity.Id,
                NationalIDNumber = entity.NationalIDNumber,
                Name = entity.Name,
                Surname = entity.Surname,
                DateOfBirth = entity.DateOfBirth,
                StudentNumber = entity.StudentNumber,
            };
        }
    }
}
