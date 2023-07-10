

using PlateaumedPro.Domain;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PlateaumedPro.Contracts
{
    public static class TeachersMapping
    {
        public static T ToModel<T>(this Teacher entity) where T : TeacherDto, new()
        {
            return new T
            {
                Id = entity.Id,
                NationalIDNumber = entity.NationalIDNumber,
                Title = entity.Title,
                Name = entity.Name,
                Surname = entity.Surname,
                DateOfBirth = entity.DateOfBirth,
                TeacherNumber = entity.TeacherNumber,
                Salary = entity.Salary,
                 
    };

        }

        public static T ToModel<T>(this TeacherDto entity) where T : Teacher, new()
        {
            return new T
            {
                Id = entity.Id,
                NationalIDNumber = entity.NationalIDNumber,
                Title = entity.Title,
                Name = entity.Name,
                Surname = entity.Surname,
                DateOfBirth = entity.DateOfBirth,
                TeacherNumber = entity.TeacherNumber,
                Salary = entity.Salary,
            };
        }
    }
}
