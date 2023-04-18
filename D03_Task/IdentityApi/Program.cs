
using IdentityApi.Data.Context;
using IdentityApi.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace IdentityApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Default
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #endregion

            #region Database
            var connectionString = builder.Configuration.GetConnectionString("myConn");
            builder.Services.AddDbContext<MainDbContext>(op=>op.UseSqlServer(connectionString));
            #endregion

            #region Idenity Managers
            builder.Services.AddIdentity<Employee, IdentityRole>(options =>
            {
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric= true;

                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<MainDbContext>();
            #endregion

            #region Authentication
            builder.Services.AddAuthentication(options =>
            {
                //Used Authentication Scheme
                options.DefaultAuthenticateScheme = "myAuthentication";

                //Used Challenge Authentication Scheme
                options.DefaultChallengeScheme = "myAuthentication";
            })
                .AddJwtBearer("myAuthentication", options =>
                {
                    var secretKeyString = builder.Configuration.GetValue<string>("SecretKey") ?? string.Empty;
                    var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKeyString);
                    var secretKey = new SymmetricSecurityKey(secretKeyInBytes);

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = secretKey,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            #endregion


            #region Authorization
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AllowEngineers", policy =>
                policy.RequireClaim(ClaimTypes.Role, "Engineering", "Management")
                .RequireClaim(ClaimTypes.NameIdentifier));

                options.AddPolicy("AllowManagers", policy =>
                policy.RequireClaim(ClaimTypes.Role, "Management")
                .RequireClaim(ClaimTypes.NameIdentifier));
            });
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}