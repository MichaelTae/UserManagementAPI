using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Models;
using UserManagementAPI.Models.CreateModels;
using UserManagementAPI.Models.Entities;
using UserManagementAPI.Models.UpdateModels;
using UserManagementAPI.Models.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public UsersController(SqlDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompleteUserViewModel>>> GetUsers()
        {
            var users = await _context.Users.Include(x => x.Location).Include(x => x.UserInfo).ToListAsync();

            return users.Select(item => new CompleteUserViewModel
                {
                    UserId = item.UserId,
                    Username = item.Username,
                    Email = item.Email,
                    FirstName = item.UserInfo?.FirstName,
                    Surname = item.UserInfo?.Surname,
                    Gender = item.UserInfo?.Gender,
                    Age = item.UserInfo?.Age ?? 0,
                    Address = item.Location?.Address,
                    Zipcode = item.Location?.Zipcode,
                    State = item.Location?.State,
                    Country = item.Location?.Country
                })
                .ToList();
        }



        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompleteUserViewModel>> GetUser(int id)
        {
            var user = await _context.Users.Include(x=> x.UserInfo).Include(x=> x.Location).FirstOrDefaultAsync(x => x.UserId == id);

            if (user == null)
            {
                return NotFound($"A user with id: {id} was not found");
            }

            return new CompleteUserViewModel
            {
                
                Email = user.Email,
                Username = user.Email,
                FirstName = user.UserInfo?.FirstName,
                Surname = user.UserInfo?.Surname,
                Gender = user.UserInfo?.Gender,
                Age = user.UserInfo?.Age,
                Address = user.Location?.Address,
                Zipcode = user.Location?.Zipcode,
                State = user.Location?.State,
                Country = user.Location?.Country,
                UserId = user.UserId,
            };

        }

        //PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> CompleteUserUpdate(int id, UpdateCompleteProfile user)
        {
            var userEntity = await _context.Users.Include(u => u.Location).Include(u => u.UserInfo).SingleOrDefaultAsync(u => u.UserId == id);

            if (userEntity == null)
            {
                return NotFound($"A user with id: {id} was not found");
            }

            userEntity.Location ??= new LocationEntity();

            userEntity.UserInfo ??= new UserInfoEntity();

            userEntity.Location.Address = user.Address;
            userEntity.Location.Zipcode = user.Zipcode;
            userEntity.Location.State = user.State;
            userEntity.Location.Country = user.Country;
            userEntity.UserInfo.FirstName = user.FirstName;
            userEntity.UserInfo.Surname = user.Surname;
            userEntity.UserInfo.Gender = user.Gender;
            userEntity.UserInfo.Age = user.Age;

            _context.Entry(userEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
               
            }

            return NoContent();
        }


        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserIncompleteModel>> CreateUser(CreateUserModel user)
        {
            
            var userEntity = new UserEntity
            {
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
              
            };
            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();


            return CreatedAtAction("GetUsers",new {id = userEntity.UserId}, new UserIncompleteModel(
                userEntity.Email,
                userEntity.Username,
                userEntity.Password));
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
