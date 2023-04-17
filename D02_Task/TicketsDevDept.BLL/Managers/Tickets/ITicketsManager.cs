using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDevDept.DAL;

namespace TicketsDevDept.BLL
{
    public interface ITicketsManager
    {
        IEnumerable<TicketReadDto> GetAllTickets();
        void AddTicket(TicketWriteDto newTicket);
        TicketReadDto? GetTicketById(int id);
        void UpdateTicket(TicketWriteDto ticket);
        void DeleteTicket(int id);
        void AssignDevsToTicket(TicketsWithDevsDto ticketsWithDevs);
    }
}
