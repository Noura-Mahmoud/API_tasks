using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TicketsDevDept.DAL;

namespace TicketsDevDept.BLL
{
    public class TicketsManager : ITicketsManager
    {
        private readonly ITicketsRepo ticketsRepo;
        private readonly IDevelopersRepo developersRepo;

        public TicketsManager(ITicketsRepo ticketsRepo, IDevelopersRepo developersRepo)
        {
            this.ticketsRepo = ticketsRepo;
            this.developersRepo = developersRepo;
        }

        public void AddTicket(TicketWriteDto newTicket)
        {
            Ticket ticket = new Ticket();
            ticket.Id = newTicket.Id;
            ticket.Title = newTicket.Title;
            ticket.Description = newTicket.Description;
            ticket.DepartmentId = newTicket.DepartmentId;
            ticketsRepo.Add(ticket);
        }

        public void DeleteTicket(int id)
        {
            ticketsRepo.Delete(id);
        }

        public IEnumerable<TicketReadDto> GetAllTickets()
        {
            var ticketsFromDb = ticketsRepo.GetAll();
            return ticketsFromDb.Select(t =>
            new TicketReadDto(Id: t.Id, Title: t.Title, Description: t.Description, DepartmentName: t.Department?.Name??"", DevelopersNames: t.Developers.Select(d=>d.Name).ToList())) ;
        }

        public TicketReadDto? GetTicketById(int id)
        {
            var ticket = ticketsRepo.GetTicketById(id);
            if (ticket != null)
            {
                TicketReadDto oneTicket = new TicketReadDto(ticket.Id, ticket.Title, ticket.Description, ticket.Department?.Name??"", ticket.Developers.Select(d=>d.Name).ToList());
                return oneTicket;
            }
            else 
                return null;
        }

        public void UpdateTicket(TicketWriteDto ticket)
        {
            Ticket ticketToBeUpdated = new Ticket();
            ticketToBeUpdated.Id = ticket.Id;
            ticketToBeUpdated.Title = ticket.Title;
            ticketToBeUpdated.Description = ticket.Description;
            ticketToBeUpdated.DepartmentId = ticket.DepartmentId;
            ticketsRepo.UpdateExistingTicket(ticketToBeUpdated);
        }

        void ITicketsManager.AssignDevsToTicket(TicketsWithDevsDto ticketsWithDevs)
        {
            //Get Ticket from Repo 
            Ticket? ticket = ticketsRepo.GetByIdWithDevs(ticketsWithDevs.TicketId);
            //Clear Ticket Devs
            if (ticket == null)
                return;
            ticket.Developers.Clear();
            //Get New Devs From Db
            ICollection<Developer> newDevs =  developersRepo.GetByDevsIds(ticketsWithDevs.DevelopersIds).ToList();
            //Assign New Devs to Ticket
            ticket.Developers = newDevs;
            //SaveChanges
            ticketsRepo.SaveChanges();
        }
    }
}
