using AFF_BE.Data;
using Microsoft.EntityFrameworkCore;

namespace AFF_BE.Api.Providers
{
    public static class EntityFrameworkProvider
    {
        public static IServiceCollection AddEntityFrameworkProvider(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<AffContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }, ServiceLifetime.Transient);

            return services;
        }
    }
}
