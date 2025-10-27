
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace E_ComerceG.Extentions
{
    public static class PresentationServicesExtentions
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {

            services.AddControllers();
            services.AddAutoMapper(typeof(Presentation.AssemblyReference).Assembly);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyHeader()
                           .AllowAnyMethod()
                           .WithOrigins("http://localhost:4200");
                });
            });

            services.AddEndpointsApiExplorer();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CostumValidationErrorResponse;   //Func ===> return IActionResult ,Take Action Context
            });
            services.ConfigureSwagger();

            return services;
        }
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "E-Commerce API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            });

            return services;
        }
    }
}
