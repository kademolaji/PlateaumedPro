using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace PlateaumedPro.Tests.Helpers
{
    public class DependencyResolverHelper
    {
        private readonly IWebHost _webHost;

        public DependencyResolverHelper(IWebHost webHost) => _webHost = webHost;

        public T GetService<T>()
        {
            using (var serviceScope = _webHost.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var scopedService = services.GetRequiredService<T>();
                    return scopedService;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public IEnumerable<T> GetServices<T>()
        {
            using (var serviceScope = _webHost.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var scopedService = services.GetServices<T>();
                    return scopedService;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}