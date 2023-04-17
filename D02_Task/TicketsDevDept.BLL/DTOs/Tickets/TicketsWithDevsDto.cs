using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDevDept.BLL
{
    public record TicketsWithDevsDto(int TicketId, int[] DevelopersIds);
}
