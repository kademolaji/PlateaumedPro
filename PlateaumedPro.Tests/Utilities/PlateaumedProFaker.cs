using PlateaumedPro.Contracts;
using PlateaumedPro.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace PlateaumedPro.Tests.Utilities
{
    public static  class PlateaumedProFaker
    {
        public static ICollection<User> GetUsers()
        {
            return new List<User>
            {
                new User
              {
                  Id =      "7f107726-562e-4b6e-9ebe-722d2b0959de",
                  ApiKey = "plateaumed_test_51JUu1MGNU5r3gGaDKd4aZKlcgy0IWF1px7EjZQlnNwfC9IRMy2uPQj3c0ZLhCLhyoHdhSFUXgewCXCN2nJeRWpro00W1qWBesM",
                  Name = "User A",
                  IsActive = true
              },
               new User
               {
                   Id =        "3be6a48b-47d4-4cdd-89d5-c02419dd73a9",
                   ApiKey = "plateaumed_test_51JUu1MGNU5r3gGaDKd4aZKlcgy0IWF1px7EjZQlnNwfC9IRMy2uPQj3c0ZLhCLhyoHdhSFUXgewCXCN2nJeRWpro00W1qLePvT",
                   Name = "User B",
                   IsActive = true
               },
                 new User
                 {
                     Id =      "8bc3f28c-e64b-4546-9083-b0dad58d1b40",
                     ApiKey = "plateaumed_test_51JUu1MGNU5r3gGaDKd4aZKlcgy0IWF1px7EjZQlnNwfC9IRMy2uPQj3c0ZLhCLhyoHdhSFUXgewCXCN2nJeRWpro00W1qAeWVe",
                     Name = "User C",
                     IsActive = true
                 },
                   new User
                   {
                       Id =     "c9d4c053-49b6-410c-bc78-2d54a9991890",
                       ApiKey = "plateaumed_test_51JUu1MGNU5r3gGaDKd4aZKlcgy0IWF1px7EjZQlnNwfC9IRMy2uPQj3c0ZLhCLhyoHdhSFUXgewCXCN2nJeRWpro00W1qreGeFe",
                       Name = "User D",
                       IsActive = true
                   }
            };
        }
        public static ICollection<StudentDto> GetStudents()
        {
            return new List<StudentDto>
            {
                new StudentDto
              {
                Id = 1,
                NationalIDNumber = "ERT1123",
                Name =  "John",
                Surname =  "Doe",
                DateOfBirth =  Convert.ToDateTime("01/02/2012"),
                StudentNumber =  "202301",
              },
               new StudentDto
               {
                  Id = 2,
                NationalIDNumber =  "ERT1246",
                Name =  "Sam",
                Surname =  "Smart",
                DateOfBirth =  Convert.ToDateTime("01/02/196"),
                StudentNumber =  "202302",
               }
            };
        }

        public static ICollection<TeacherDto> GetTeachers()
        {
            return new List<TeacherDto>
            {
                new TeacherDto
              {
                   Id = 1,
                NationalIDNumber = "ERT1123",
                Name =  "John",
                Surname =  "Doe",
                DateOfBirth =  Convert.ToDateTime("01/02/2012"),
                TeacherNumber =  "202301",
                Title = "Head Teacher",
                Salary = 500000
              },
               new TeacherDto
               {
                Id = 1,
                NationalIDNumber = "ERT1123",
                Name =  "John",
                Surname =  "Doe",
                DateOfBirth =  Convert.ToDateTime("01/02/2012"),
                TeacherNumber =  "202301",
                Title = "Teacher",
                Salary = 20000
               }
            };
        }
    }
}
