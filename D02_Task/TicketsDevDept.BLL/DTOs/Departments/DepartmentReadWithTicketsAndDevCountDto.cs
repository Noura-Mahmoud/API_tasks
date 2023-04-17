using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDevDept.BLL
{
    public record DepartmentReadWithTicketsAndDevCountDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public IEnumerable<TicketWithDevCountDto> Tickets { get; init; } = new List<TicketWithDevCountDto>();
    }
}
