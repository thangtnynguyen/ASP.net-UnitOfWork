namespace AFF_BE.Api.Providers
{
    public static class AutoMapperProvider
    {
        public static IServiceCollection AddAutoMapperProvider(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program));

            return services;
        }
    }
}
