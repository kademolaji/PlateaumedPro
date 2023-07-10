using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlateaumedPro.Services;

namespace PlateaumedPro.Tests.Services
{
    public class HttpAccessorServiceTest
    {
        [Fact]
        public void TestHttpAccessorServiceShouldReturnTheCorrectHostAddress()
        {
            // Given
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var context = new DefaultHttpContext();

            UriHelper
                 .FromAbsolute("http://plateaumed.com", out var scheme, out var host, out var path, out var query,
                   fragment: out var _);

            context.Request.Scheme = scheme;
            context.Request.Host = host;
            context.Request.Path = path;
            context.Request.QueryString = query;
            mockHttpContextAccessor.Setup(_ => _.HttpContext).Returns(context);
            var httpAccessorService = new HttpAccessorService(mockHttpContextAccessor.Object);

            // When
            var hostAddress = httpAccessorService.GetHostAddress();
            var expextedAddress = "plateaumed.com";

            // Then
            Assert.NotNull(hostAddress);
            Assert.Equal(hostAddress, expextedAddress);
        }
    }
}

