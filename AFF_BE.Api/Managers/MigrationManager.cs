using AFF_BE.Data;
using Microsoft.EntityFrameworkCore;

namespace AFF_BE.Api.Managers
{
    public static class MigrationManager
    {
        public static WebApplication MigrateDatabase(this WebApplication app)
        {
            using(var scope = app.Services.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<AffContext>())
                {
                    context.Database.Migrate();
                    //new DataSeeder().SeedAsync(context).Wait();
                }
            }
            return app;
        }
    }
}
