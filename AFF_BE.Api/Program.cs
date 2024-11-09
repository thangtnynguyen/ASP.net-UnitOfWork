using AFF_BE.Api.Hubs;
using AFF_BE.Api.Managers;
using AFF_BE.Api.Providers;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);


// Provider
builder.Services.AddSignalR();
builder.Services.AddAppProvider();
builder.Services.AddEntityFrameworkProvider(builder);
builder.Services.AddIdentityProvider(builder);
builder.Services.AddFluentValidationProvider();
builder.Services.AddDependencyInjectionProvider();
builder.Services.AddSwaggerProvider();
builder.Services.AddAutoMapperProvider();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScopedProvider();




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("CorsPolicy");

#region ignore
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
#endregion

if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AFF SMO - TNY NGUYEN v1");
        c.DisplayOperationId();
        c.DisplayRequestDuration();
    });
}


app.UseStaticFiles();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


//app.MigrateDatabase();

#pragma warning disable ASP0014 
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<RefreshTokenHub>("/hubs/refresh-token-hub");
});
#pragma warning restore ASP0014 

app.Run();
