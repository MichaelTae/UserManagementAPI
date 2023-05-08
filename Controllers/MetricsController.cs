using System.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Models.ViewModels;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]/users")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public MetricsController(SqlDbContext context)
        {
            _context = context;
        }

        // GET: api/Metrics
        [HttpGet]
        public IActionResult GetMetrics()
        {
            var users =  _context.Users.Count();
            var tickets = _context.Tickets.Count();
            var bookings = _context.UserTickets.Count();
            return Ok(new {users, tickets, bookings});
        }

        [HttpGet]
        [Route("age")]

        public async Task<ActionResult<IEnumerable<UserAgeViewModel>>> GetUsersByAge()
        {
            var users = await _context.Users.Include(x => x.UserInfo).ToListAsync();

            // Define age ranges
            var ageRanges = new List<(int Min, int Max, string Label)>
            {
                (0, 20, "0-20"),
                (21, 30, "21-30"),
                (31, 40, "31-40"),
                (41, 50, "41-50"),
                (51, 60, "51-60"),
                (61, 70, "61-70"),
                (71, 80, "71-80"),
                (81, 90, "81-90"),
                (91, 100, "91-100")
               
            };

            // Group users by age ranges and count them
            var groupedUsers = ageRanges
                .Select(range => new UserAgeViewModel
                {
                    AgeSpan = range.Label,
                    Quantity = users.Count(user => user.UserInfo?.Age >= range.Min && user.UserInfo?.Age <= range.Max)
                })
                .Where(group => group.Quantity > 0)
                .ToList();

            return groupedUsers;
        }


        [HttpGet]
        [Route("Country")]
        public async Task<ActionResult<IEnumerable<UserCountryViewModel>>> GetUsersByCountry()
        {
            var users = await _context.Users.Include(x => x.Location).ToListAsync();
            // Group users by country and count them
            var groupedUsers = users
                .GroupBy(user => user.Location?.Country)
                .Select(group => new UserCountryViewModel
                {
                    Country = group.Key,
                    Quantity = group.Count()
                })
                .Where(group => group.Quantity > 0)
                .ToList();
            return groupedUsers;
        }

        [HttpGet]
        [Route("Gender")]

        public async Task<ActionResult<IEnumerable<UserGenderViewModel>>> GetUsersByGender()
        {
            var users = await _context.Users.Include(x => x.UserInfo).ToListAsync();

            var groupedUsers = users.GroupBy(user => user.UserInfo?.Gender)
                .Select(group => new UserGenderViewModel
                {
                    Gender = group.Key,
                    Quantity = group.Count()
                })
                .Where(group => group.Quantity > 0)
                .ToList();
            return groupedUsers;

        }


        [HttpGet]
        [Route("Revenue/PerTicket")]
        public async Task<ActionResult<IEnumerable<TicketsRevenueViewModel>>> GetRevenuePerTicket()
        {
            var tickets = await _context.UserTickets.Include(x=> x.Ticket).ToListAsync();

            var groupedTickets = tickets.GroupBy(ticket => ticket.Ticket.TicketName)
                .Select(group => new TicketsRevenueViewModel
                {
                    TicketName = group.Key,
                    Revenue = group.Sum(ticket => ticket.Ticket.Price)
                })
                .Where(group => group.Revenue > 0)
                .ToList();
            return groupedTickets;
        }

        [HttpGet]
        [Route("Revenue/PerUser")]

        public async Task<ActionResult<IEnumerable<UserRevenueViewModel>>> GetRevenuePerUser()
        {
            var users = await _context.UserTickets.Include(x => x.User).Include(y=> y.Ticket).ToListAsync();
            var groupedUsers = users.GroupBy(user => user.User.Email)
                .Select(group => new UserRevenueViewModel
                {
                    Email = group.Key,
                    Revenue = group.Sum(user => user.Ticket.Price)
                })
                .Where(group => group.Revenue > 0)
                .ToList();
            return groupedUsers;
        }

        [HttpGet]
        [Route("Revenue/Total")]
        public async Task<IActionResult> GetTotalRevenue()
        {
            var tickets = await _context.UserTickets.Include(x => x.Ticket).ToListAsync();
            var totalRevenue = tickets.Sum(ticket => ticket.Ticket.Price);
            return Ok(new {totalRevenue});
        }


    }

    
}
