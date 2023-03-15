using API.Models.DB;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    //[Route("users")]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IEnumerable<User> Get()
        {
            //Return a list of users.
            //Here we have to have a service that will retrieve data from DB
            return new List<User>() { new User() { Id = 1, Username = "Kalin", DateOfBirth = new DateTime(1991, 03, 11) } };
        }

        [HttpGet]
        [Route("{id:int}")]
        public User GetById(int id)
        {
            return new User() { Id = 1, Username = "Kalin" };
        }

        [HttpPost]
        public void Post([FromBody] User user)
        {

           Console.WriteLine(user);
        }

        
    }
}
