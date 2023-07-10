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
    public class TeacherControllerTest : BaseServiceTest
    {
        [Fact]
        public void TestTeacherControllerShouldReturnBadRequestObjectResult()
        {
            // Given


            var teacherService = new Mock<ITeacherService>();
            var loggerServiceMock = new Mock<ILoggerService>();
            var httpAccessorServiceMock = new Mock<IHttpAccessorService>();
            var teacherController = new TeacherController(teacherService.Object, httpAccessorServiceMock.Object, loggerServiceMock.Object);
            var teachers = PlateaumedProFaker.GetTeachers();
            var teacher = teachers.FirstOrDefault();

            // When
            var order = teacherController.Create(teacher).GetAwaiter().GetResult();
            // Then
            Assert.IsType<BadRequestObjectResult>(order);
        }
    }
}
