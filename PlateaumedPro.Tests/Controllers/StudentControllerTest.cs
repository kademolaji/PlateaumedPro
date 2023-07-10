using Microsoft.AspNetCore.Mvc;
using Moq;
using PlateaumedPro.Common;
using PlateaumedPro.Contracts;
using PlateaumedPro.Tests.Services;
using PlateaumedPro.Tests.Utilities;
using PlateaumedPro.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateaumedPro.Tests.Controllers
{
    public class StudentControllerTest : BaseServiceTest
    {
        [Fact]
        public void TestStudentControllerShouldReturnBadRequestObjectResult()
        {
            // Given

            var studentService = new Mock<IStudentService>();
            var loggerServiceMock = new Mock<ILoggerService>();
            var httpAccessorServiceMock = new Mock<IHttpAccessorService>();
            var studentController = new StudentController(studentService.Object, httpAccessorServiceMock.Object, loggerServiceMock.Object);
            var students = PlateaumedProFaker.GetStudents();
            var student = students.FirstOrDefault();

            // When
            var order = studentController.Create(student).GetAwaiter().GetResult();
            // Then
            Assert.IsType<BadRequestObjectResult>(order);
        }
    }
}

