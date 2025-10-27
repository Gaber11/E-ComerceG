
namespace E_ComerceG.Extentions
{
    public static class WebApplicationExtentions
    {
        public static async Task<WebApplication> SeedDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbintializer = scope.ServiceProvider.GetRequiredService<IDBInitializer>();
            await dbintializer.InitializeAsync();
            await dbintializer.InitializeIdentityAsync();
            return app;
        }

        public static WebApplication CustomMiddleWare(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }

    }
}
