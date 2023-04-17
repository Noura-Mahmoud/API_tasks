using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDevDept.DAL
{
    public class TicketsRepo : ITicketsRepo
    {
        private readonly Context context;

        public TicketsRepo(Context context)
        {
            this.context = context;
        }

        public void Add(Ticket newTicket)
        {
            try
            {
                context.Tickets.Add(newTicket);
                SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);   
            }
        }

        public void Delete(int id)
        {
            var ticket = context.Tickets.FirstOrDefault(t => t.Id == id);
            if (ticket != null)
                context.Tickets.Remove(ticket);
            SaveChanges();
        }

        public IEnumerable<Ticket> GetAll()
        {
            return context.Tickets.Include(t=>t.Department).Include(t=>t.Developers).ToList();
        }

        public Ticket? GetTicketById(int id)
        {
            return context.Tickets.Include(t => t.Department).Include(t => t.Developers).FirstOrDefault(t => t.Id == id);
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void UpdateExistingTicket(Ticket ticket)
        {
            var updatingTicket = context.Tickets.FirstOrDefault(t => t.Id == ticket.Id);
            if (updatingTicket != null)
            {
                updatingTicket.Title = ticket.Title;
                updatingTicket.Description = ticket.Description;
            }
            SaveChanges();
        }

        Ticket? ITicketsRepo.GetByIdWithDevs(int id)
        {
            return context.Set<Ticket>()
                .Include(t => t.Developers).ToList().FirstOrDefault(t => t.Id == id);
        }
    }
}
