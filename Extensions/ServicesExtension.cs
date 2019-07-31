using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using LiveTogether.Utils.Configuration;
using LiveTogether.Data;
using LiveTogether.Data.Repositories;

namespace LiveTogether.Extensions
{
    public static class ServicesExtension
    {
        public static void ConfigureDatabaseConnection(this IServiceCollection services, string dbConnection)
        {
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<LiveTogetherContext>(options =>
                    options.UseNpgsql(dbConnection)
                );
        }

        public static void ConfigureAuthentication(this IServiceCollection services, AppSettings appSettings)
        {
            var secret = appSettings.Secret;
            var secretKey = Encoding.ASCII.GetBytes(secret);

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userRep = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = userRep.GetById(userId);

                        if(user == null) {
                            context.Fail("Unauthorized");
                        }

                        return Task.CompletedTask;
                    }
                };

                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}