using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDevDept.DAL;

namespace TicketsDevDept.BLL
{
    public class DepartmentsManager : IDepartmentsManager
    {
        private readonly IDepartmentsRepo departmentsRepo;

        public DepartmentsManager(IDepartmentsRepo departmentsRepo)
        {
            this.departmentsRepo = departmentsRepo;
        }
        public DepartmentReadWithTicketsAndDevCountDto? GetDeptWithTicketsAndDevs(int id)
        {
            var DeptFromDb = departmentsRepo.GetDeptDetailsWithTicketsAndDevCount(id);
            if (DeptFromDb == null)
                return null;
            else
            {
                var Dept = new DepartmentReadWithTicketsAndDevCountDto {
                    Id = DeptFromDb.Id,
                    Name = DeptFromDb.Name,
                    Tickets = DeptFromDb.Tickets.Select(t => new TicketWithDevCountDto(
                        t.Id, t.Description, t.Developers.Count())).ToHashSet()
                };
                return Dept;
            }
        }
    }
}
