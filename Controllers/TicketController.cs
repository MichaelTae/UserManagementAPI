using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Models.CreateModels;
using UserManagementAPI.Models.Entities;
using UserManagementAPI.Models.UpdateModels;
using UserManagementAPI.Models.ViewModels;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public TicketController(SqlDbContext context)
        {
            _context = context;
        }

        // GET: api/Ticket
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketEntity>>> GetTickets()
        {
            return await _context.Tickets.ToListAsync();

        }

        // GET: api/Ticket/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketEntity>> GetTicketEntity(int id)
        {
            if (_context.Tickets == null)
            {
                return NotFound();
            }
            var ticketEntity = await _context.Tickets.FindAsync(id);

            if (ticketEntity == null)
            {
                return NotFound();
            }

            return ticketEntity;
        }

        // PUT: api/Ticket/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketEntity(int id, UpdateTicketModel model)
        {
            var updatedTicket = await _context.Tickets.FindAsync(id);
            if (!ModelState.IsValid || !TicketEntityExists(id))
            {
                return BadRequest();
            }

            if (updatedTicket != null)
            {
                updatedTicket.TicketName = model.TicketName;
                updatedTicket.Location = model.Location;
                updatedTicket.Price = model.Price;
                updatedTicket.DateUpdated = DateTime.Now;

                _context.Entry(updatedTicket).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketEntityExists(id))
                {
                    return NotFound();
                }

            }

            return NoContent();
        }

        // POST: api/Ticket
        
        [HttpPost]
        public async Task<ActionResult<TicketEntity>> PostTicketEntity(CreateTicketModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var createdTicket = new TicketEntity()
            {
                TicketName = model.TicketName,
                Location = model.Location,
                Price = model.Price
            };

            _context.Tickets.Add(createdTicket);
            await _context.SaveChangesAsync();
            var newTicket = await _context.Tickets.FirstOrDefaultAsync(x => x.TicketName == model.TicketName);

            return CreatedAtAction(
                "GetTicketEntity",
                new { id = createdTicket.TicketId },
                new TicketViewModel(
                    newTicket.TicketId,
                    newTicket.TicketName,
                    newTicket.Location,
                    newTicket.DateCreated,
                    newTicket.Price
                )
            );
        }

        // DELETE: api/Ticket/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketEntity(int id)
        {
            if (!TicketEntityExists(id))
            {
                return NotFound("A Ticket with that Id does not exist.");
            }
            var ticketEntity = await _context.Tickets.FindAsync(id);
            if (ticketEntity == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticketEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketEntityExists(int id)
        {
            return (_context.Tickets?.Any(e => e.TicketId == id)).GetValueOrDefault();
        }
    }
}
