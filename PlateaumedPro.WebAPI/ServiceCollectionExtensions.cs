using PlateaumedPro.Common;
using PlateaumedPro.Contracts;
using PlateaumedPro.Domain;
using PlateaumedPro.Services;

namespace PlateaumedPro.WebAPI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // Configure singletons services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ILoggerService, LoggerService>();

            // Configure DI for application services
            services.AddScoped<IStudentService, StudentServices>();
            services.AddScoped<ITeacherService, TeacherServices>();
            services.AddScoped<IAuditTrailService, AuditTrailService>();
            services.AddScoped<IHttpAccessorService, HttpAccessorService>();
            services.AddScoped<IBasicAuthService, BasicAuthService>();

            // Configure transcient services
            services.AddTransient<DataContext>();

            return services;
        }
    }
}
