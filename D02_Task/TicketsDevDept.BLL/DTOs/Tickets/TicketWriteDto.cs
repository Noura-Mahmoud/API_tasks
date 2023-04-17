using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDevDept.BLL
{
    public record TicketWriteDto(int Id, string Title, string Description, int DepartmentId);
}
