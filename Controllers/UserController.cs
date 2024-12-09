using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Users.Database;
using Users.Models;

namespace Users.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        public UserController(ApplicationDBContext dBContext)
        { 
            _dbContext = dBContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _dbContext.Users.ToList();

            return Ok(users);
        }

        [HttpGet("id")]
        public IActionResult GetbyId(int id)
        { 
            var user = _dbContext.Users.Find(id);

            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody]UserModel user)
        {
            using (var tranaction = _dbContext.Database.BeginTransaction())
            {
                //Setting this Identity ON, to update the primary key
                _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Users ON");

                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

                _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Users OFF");

                tranaction.Commit();
            }          

            return Ok();
        }

        [HttpPut("id")]
        public IActionResult UpdateUser(int id, [FromBody]UserModel user)
        {
            var existingUser = _dbContext.Users.Find(id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.MobileNumber = user.MobileNumber;

                _dbContext.Users.Update(existingUser);
                _dbContext.SaveChanges();
            }

            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteUser(int id)
        {
            var user = _dbContext.Users.Find(id);
            if (user != null) 
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
            return NoContent();
        }
    }
}
