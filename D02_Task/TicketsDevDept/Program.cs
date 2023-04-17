
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicketsDevDept.BLL;
using TicketsDevDept.DAL;

namespace TicketsDevDept
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region DB
            var connectionSting = builder.Configuration.GetConnectionString("ticketConn");
            builder.Services.AddDbContext<Context>(op => op.UseSqlServer(connectionSting));
            #endregion

            #region Repos
            builder.Services.AddScoped<ITicketsRepo, TicketsRepo>();
            builder.Services.AddScoped<IDepartmentsRepo, DepartmentsRepo>();
            builder.Services.AddScoped<IDevelopersRepo, DevelopersRepo>();
            #endregion

            #region Manager
            builder.Services.AddScoped<ITicketsManager, TicketsManager>();
            builder.Services.AddScoped<IDepartmentsManager, DepartmentsManager>();
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}