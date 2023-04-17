using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TicketsDevDept.DAL
{
    public class Context: DbContext
    {
        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<Developer> Developers => Set<Developer>(); 
        public DbSet<Department> Departments => Set<Department>();

        public Context(DbContextOptions<Context> options):base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var developers = JsonSerializer.Deserialize<List<Developer>>("""
                [  {    "Id": 1,    "Name": "John Smith"  },
                {    "Id": 2,    "Name": "Jane Doe"  },
                {    "Id": 3,    "Name": "Bob Johnson"  },
                {    "Id": 4,    "Name": "Lisa Lee"  }]
                """) ?? new();
            var departments = JsonSerializer.Deserialize<List<Department>>("""
                [  {    "Id": 1,    "Name": "Sales"  },
                {    "Id": 2,    "Name": "Marketing"  },
                {    "Id": 3,    "Name": "IT"  },
                {    "Id": 4,    "Name": "Human Resources"  }]
                """) ?? new();
            var tickets = JsonSerializer.Deserialize<List<Ticket>>("""
                [  {    "Id": 1,    "Description": "Cannot access email",    "Title": "Email issue",    "DepartmentId": 3  },
                {    "Id": 2,    "Description": "Printer not working",    "Title": "Printer issue",    "DepartmentId": 3  },
                {    "Id": 3,    "Description": "Application crashing",    "Title": "Application issue",    "DepartmentId": 3  },
                {    "Id": 4,    "Description": "New employee onboarding",    "Title": "Onboarding request",    "DepartmentId": 4  },
                {    "Id": 5,    "Description": "Need access to specific folder",    "Title": "Access request",    "DepartmentId": 3  }]
                """) ?? new();
            modelBuilder.Entity<Developer>().HasData(developers);
            modelBuilder.Entity<Department>().HasData(departments);
            modelBuilder.Entity<Ticket>().HasData(tickets);

        }

    }
}
