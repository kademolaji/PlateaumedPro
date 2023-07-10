using PlateaumedPro.Services;
using PlateaumedPro.Tests.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateaumedPro.Tests.Services
{
    public class BasicAuthServiceTest : BaseServiceTest
    {
        [Fact]
        public void TestCheckValidUserKeyShouldReturnFalse()
        {
            // Given
            var users = PlateaumedProFaker.GetUsers();
            foreach (var user in users)
            {
                MockContext.Users.Add(user);
            }

            MockContext.SaveChanges();

            // When

            var basicAuthService = new BasicAuthService(MockContext);
            var result = basicAuthService.CheckValidUserKey("");

            // Then
            Assert.False(result);
        }

        [Fact]
        public void TestCheckValidUserKeyShouldReturnTrue()
        {
            // Given
            var users = PlateaumedProFaker.GetUsers();
            foreach (var user in users)
            {
                MockContext.Users.Add(user);
            }

            MockContext.SaveChanges();
            var myChannel = users.FirstOrDefault();

            // When

            var basicAuthService = new BasicAuthService(MockContext);
            var result = basicAuthService.CheckValidUserKey(myChannel.ApiKey);

            // Then
            Assert.True(result);
        }
    }
}

