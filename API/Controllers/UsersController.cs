using API.Contracts.Services;
using API.Models.DB;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    //[Route("users")]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }


        [HttpGet]
        [Route("{id:int}")]
        public User GetById(int id)
        {
            var user = _usersService.GetUserById(id);
            return user;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetUsers()
        {
            var users = _usersService.GetUsers();

            return Ok(users);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            var success = _usersService.CreateUser(user);

            return Ok(success);
        }

        
    }
}
