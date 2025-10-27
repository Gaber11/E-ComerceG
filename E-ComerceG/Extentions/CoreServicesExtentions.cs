
using Service_Abstraction;
using Shared;

namespace E_ComerceG.Extentions
{
    public static class CoreServicesExtentions
    {

        public static IServiceCollection AddCoreServices(this IServiceCollection services , IConfiguration configuration)
        {
            // Add core services here in the future
            services.AddScoped<IServiceManager, ServiceManager>();

            services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            return services;


        }

    }
}
