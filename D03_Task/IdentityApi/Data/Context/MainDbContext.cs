using IdentityApi.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Data.Context
{
    public class MainDbContext : IdentityDbContext<Employee>
    {
        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Employee>().ToTable("Employees");
            builder.Entity<IdentityUserClaim<string>>().ToTable("EmployeesClaims");
        }

    }
}
