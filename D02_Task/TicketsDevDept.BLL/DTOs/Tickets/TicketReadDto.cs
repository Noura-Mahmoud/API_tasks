using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDevDept.DAL;

namespace TicketsDevDept.BLL
{
    public record TicketReadDto(int Id, string Title, string Description, string DepartmentName, IEnumerable<string> DevelopersNames);
}
