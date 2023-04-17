using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDevDept.DAL
{
    public interface ITicketsRepo
    {
        IEnumerable<Ticket> GetAll();
        void Add(Ticket newTicket);
        void Delete(int id);
        Ticket? GetTicketById(int id);
        Ticket? GetByIdWithDevs (int id);
        void UpdateExistingTicket(Ticket ticket);
        int SaveChanges();
    }
}
