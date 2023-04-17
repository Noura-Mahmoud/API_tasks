using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketsDevDept.BLL;

namespace TicketsDevDept.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketsManager ticketsManager;

        public TicketsController(ITicketsManager ticketsManager)
        {
            this.ticketsManager = ticketsManager;
        }
        [HttpGet]
        public ActionResult <List<TicketReadDto>> GetAll() 
        {
            return ticketsManager.GetAllTickets().ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<TicketReadDto> GetById(int id)
        {
            var ticket = ticketsManager.GetTicketById(id);
            if (ticket == null)
                return NotFound();
            else
                return ticket;
        }

        [HttpPost]
        public ActionResult AddNewTicket(TicketWriteDto newTicket)
        {
            ticketsManager.AddTicket(newTicket);
            return NoContent();
        }

        [HttpPut]
        public ActionResult EditTicket(TicketWriteDto Ticket) 
        {
            ticketsManager.UpdateTicket(Ticket);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteTicket(int id)
        {
            ticketsManager.DeleteTicket(id);
            return NoContent();
        }

        [HttpPost]
        [Route("AssignDevlopersToTicket")]
        public ActionResult AssignDevlopersToTicket(TicketsWithDevsDto ticketsWithDevs)
        {
            ticketsManager.AssignDevsToTicket(ticketsWithDevs);
            return NoContent();
        }
    }
}
