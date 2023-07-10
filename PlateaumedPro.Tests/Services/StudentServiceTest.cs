using Moq;
using PlateaumedPro.Common;
using PlateaumedPro.Contracts;
using PlateaumedPro.Services;
using PlateaumedPro.Tests.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateaumedPro.Tests.Services
{
    public class StudentServiceTest : BaseServiceTest
    {
        [Fact]
        public void TestStudentServiceShouldReturnTrueStatusGivenRightInput()
        {
           
            var auditTrailServiceMock = new Mock<IAuditTrailService>();
            var loggerServiceMock = new Mock<ILoggerService>();
            var createOrderService = new StudentServices(MockContext, auditTrailServiceMock.Object);
            var students = PlateaumedProFaker.GetStudents();
            var studentData = students.FirstOrDefault();
            studentData.Id = 0;
            var expextedMessage = "Record created successfully";


            // When
            var order = createOrderService.CreateStudent(studentData).GetAwaiter().GetResult();
            // Then
            Assert.True(order.ResponseType.Status);
            Assert.Equal(order.ResponseType.Message, expextedMessage);
        }

       
    }
}
