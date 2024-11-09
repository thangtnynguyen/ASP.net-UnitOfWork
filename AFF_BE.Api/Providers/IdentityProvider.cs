using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AFF_BE.Data;
using AFF_BE.Core.Data.Identity;
using System.Reflection;
using AFF_BE.Core.Constants.Identity;


namespace AFF_BE.Api.Providers
{
    public static class IdentityProvider
    {
        public static IServiceCollection AddIdentityProvider(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddIdentity<User,Role>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.AllowedUserNameCharacters = null;
            }).AddEntityFrameworkStores<AffContext>().AddDefaultTokenProviders();

            var jwtKey = builder.Configuration.GetValue<string>("JwtTokenSettings:Key");
            var audience = builder.Configuration.GetValue<string>("JwtTokenSettings:Audience");
            var issuer = builder.Configuration.GetValue<string>("JwtTokenSettings:Issuer");

            var key = Encoding.ASCII.GetBytes(jwtKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,

                    ValidAudience = audience,
                    ValidIssuer = issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };
            });

            var permissionType = typeof(PermissionConstant);
            var permissionFields = permissionType.GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.IsLiteral && !f.IsInitOnly)
            .Select(f => (string)f.GetValue(null))
            .ToList();

            services.AddAuthorization(options =>
            {
                foreach (var permission in permissionFields)
                {
                    options.AddPolicy(permission, policy =>
                    {
                        //policy.RequireClaim("Permission", permission);
                        policy.RequireAssertion(context =>
                        {
                            var hasAdminOrMaster = context.User.HasClaim(c =>
                                c.Type == "Permission" && (c.Value == PermissionConstant.Admin || c.Value ==PermissionConstant.Master));

                            if (hasAdminOrMaster)
                            {
                                return true;
                            }
                            return context.User.HasClaim("Permission", permission);
                        });
                    });
                }
            });

            return services;
        }
    }
}
