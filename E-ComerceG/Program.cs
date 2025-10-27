


namespace E_ComerceG
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Services
            //Presentation Services
            builder.Services.AddPresentationServices();
            //Infrastructure Services
            builder.Services.AddInfrastructureServices(builder.Configuration);
            //core services
            builder.Services.AddCoreServices(builder.Configuration); 
            #endregion

            builder.Services.AddControllers()
              .AddJsonOptions(options =>
              {
                  //Text enum values instead of numeric values in the response
                  options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
              });

            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            #region MiddleWares / PipLines
            app.CustomMiddleWare();
            await app.SeedDbAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run(); 
            #endregion

        }
    }
}
