using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using PlateaumedPro.Contracts;
using PlateaumedPro.Tests.Helpers;
using Xunit;


namespace PlateaumedPro.Tests.DependencyInjection
{
    public class WebApiDependencyResolverTest
    {
        private const string SettingsFile = "appsettings.json";
        private readonly DependencyResolverHelper _serviceProvider;

        public WebApiDependencyResolverTest()
        {

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(SettingsFile, optional: false, reloadOnChange: false)
                .Build();

            var webHost = WebHost.CreateDefaultBuilder()
                .UseConfiguration(config)
                .Build();

            _serviceProvider = new DependencyResolverHelper(webHost);
        }

        [Fact]
        public void ICreateOrderServiceShouldGetResolved()
        {
            // When
            var service = _serviceProvider.GetService<IStudentService>();

            // Then
            Assert.NotNull(service);
        }

        [Fact]
        public void IHttpAccessorServiceShouldGetResolved()
        {
            // When
            var service = _serviceProvider.GetService<IHttpAccessorService>();

            // Then
            Assert.NotNull(service);
        }

        [Fact]
        public void IAuditTrailServiceShouldGetResolved()
        {
            // When
            var service = _serviceProvider.GetService<IAuditTrailService>();

            // Then
            Assert.NotNull(service);
        }

        [Fact]
        public void IBasicAuthServiceShouldGetResolved()
        {
            // When
            var service = _serviceProvider.GetService<IBasicAuthService>();

            // Then
            Assert.NotNull(service);
        }
    }
}
