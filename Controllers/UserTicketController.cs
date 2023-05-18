using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Models.CreateModels;
using UserManagementAPI.Models.Entities;
using UserManagementAPI.Models.ViewModels;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTicketController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public UserTicketController(SqlDbContext context)
        {
            _context = context;
        }

        private static UserTicketViewModel ToUserTicketViewModel(UserTicketEntity userTicketEntity)
        {
            return new UserTicketViewModel
            {
                BookingId = userTicketEntity.BookingId,
                UserId = userTicketEntity.UserId,
                TicketId = userTicketEntity.TicketId,
                UserName = userTicketEntity.User.Username,
                TicketName = userTicketEntity.Ticket.TicketName,
                Location = userTicketEntity.Ticket.Location,
                DateCreated = userTicketEntity.Ticket.DateCreated,
                DateUpdated = userTicketEntity.Ticket.DateUpdated,
                Price = userTicketEntity.Ticket.Price
            };
        }


        // GET: api/UserTicket
        [HttpGet]
        public async Task<ActionResult<UserTicketViewModel>> GetUserTickets()
        {
            var userTicketEntities = await _context.UserTickets
                .Include(x => x.User)
                .Include(x => x.Ticket)
                .ToListAsync();

            if (userTicketEntities.Count == 0)
            {
                return NotFound();
            }

            var userTicketViewModels = userTicketEntities.Select(ToUserTicketViewModel).ToList();

            return Ok(userTicketViewModels);
        }


        // GET: api/UserTicket/5

        /// <summary>
        ///  Get a user ticket by userId or ticketId
        ///  1. Get a user ticket by userId or ticketId from the database
        ///  2. Return a user ticket
        ///  3. If there is no user ticket, return 404 Not Found
        ///  
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ticketId"></param>
        /// <returns>UserTicketViewModel</returns>

        [HttpGet("/api/UserTicket/ByUserIdOrTicketId")]
        public async Task<ActionResult<UserTicketViewModel>> GetUserTicketEntities(
            [FromQuery(Name = "userId")] int? userId = null, [FromQuery(Name = "ticketId")] int? ticketId = null)
        {
            if (userId == null && ticketId == null)
            {
                return BadRequest("Either userId or ticketId must be provided.");
            }

            IQueryable<UserTicketEntity> query = _context.UserTickets;

            var userTicketEntities = await _context.UserTickets
                .Include(x => x.User)
                .Include(x => x.Ticket)
                .Where(x => x.UserId == userId || x.TicketId == ticketId)
                .ToListAsync();

            if (userId != null)
            {
                query = query.Where(ut => ut.UserId == userId);
            }

            if (ticketId != null)
            {
                query = query.Where(ut => ut.TicketId == ticketId);
            }

            if (userTicketEntities.Count == 0)
            {
                return NotFound();
            }

            var userTicketViewModels = userTicketEntities.Select(ToUserTicketViewModel).ToList();

            return Ok(userTicketViewModels);
        }




        // POST: api/UserTicket
        /// <summary>
        ///  A User ticket created by linking a user to a ticket, a user can have many tickets  and a ticket can have many users. 
        ///  1. Create a user ticket
        ///  2. Return a user ticket
        ///  3. If the model is invalid, return 400 Bad Request
        ///  4. If the user ticket already exists, return 409 Conflict
        ///  5. If the user or ticket does not exist, return 404 Not Found
        ///  
        /// </summary>
        /// <param name="model"></param>
        /// <returns> CreateUserTicketEntity </returns>
        [HttpPost]
        public async Task<ActionResult<CreateUserTicketEntity>> PostUserTicketEntity(CreateUserTicketEntity model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userTicketEntity = new UserTicketEntity
            {
                UserId = model.UserId,
                TicketId = model.TicketId,
                  
            };
            _context.UserTickets.Add(userTicketEntity);
            await _context.SaveChangesAsync();

               

            return CreatedAtAction("GetUserTickets", new { id = userTicketEntity.BookingId }, model);

        }

        // DELETE: api/UserTicket/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserTicketEntity(int id)
        {
           
            var userTicketEntity = await _context.UserTickets.FindAsync(id);
            if (!UserTicketEntityExists(id))
            {
                return NotFound($"A user with id: {id} was not found.");
            }

            _context.UserTickets.Remove(userTicketEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserTicketEntityExists(int id)
        {
            return (_context.UserTickets?.Any(e => e.BookingId == id)).GetValueOrDefault();
        }
    }
}
