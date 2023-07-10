using Moq;
using PlateaumedPro.Common;
using PlateaumedPro.Contracts;
using PlateaumedPro.Domain;
using PlateaumedPro.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateaumedPro.Tests.Services
{
    public class AuditTrailServiceTest : BaseServiceTest
    {
        [Fact]
        public void TestAuditTrailServiceShouldReturnTheCountOfInsertedData()
        {
            // Given

            var httpAccessorServiceMock = new Mock<IHttpAccessorService>();
            var auditTrailService = new AuditTrailService(MockContext, httpAccessorServiceMock.Object);
            int expectedCount = 1;

             auditTrailService.SaveAuditTrail("Test Test Test", "Student", ActionType.Created, "Tester").GetAwaiter().GetResult();

            // When
            var auditTrails = auditTrailService.SearchAuditTrail(new SearchCall<string> { From = 0, PageSize = 10, Parameter = "" }).GetAwaiter().GetResult();

            // Then
            Assert.Equal(auditTrails.ResponseType.TotalCount, expectedCount);
        }
    }
}

