using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDevDept.DAL
{
    public class DepartmentsRepo : IDepartmentsRepo
    {
        private readonly Context context;

        public DepartmentsRepo(Context context)
        {
            this.context = context;
        }
        public Department? GetDeptDetailsWithTicketsAndDevCount(int id)
        {
            //var dept = context.Departments.Include(d => d.Tickets).ThenInclude
            var deptWithTickets = context.Departments
                .Include(d => d.Tickets).Select(d=>
                new Department{
                    Id = d.Id,
                    Name = d.Name,
                    Tickets = (ICollection<Ticket>)d.Tickets.Select(t=>new Ticket
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        Developers = t.Developers
                    }),
                    }
                ).FirstOrDefault(d => d.Id == id);
            return deptWithTickets;
        }
    }
}
