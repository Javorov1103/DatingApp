using API.Contracts;
using API.Contracts;
using API.Models;
using API.Models.DB;
using API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<LoginResponseDTO> Login([FromBody]LoginUserDTO model)
        {
            LoginResponseDTO user = this._authService.Login(model);

            if (user == null)
            {
                return Unauthorized();
            }

            return user;
        }

        [HttpPost("register")] //POST 
        public ActionResult<User> Register([FromBody] RegisterUserDTO model) 
        {
            //Check the username is it taken

            //create the user in the db

            //Create a login token and turn it back with other info if its needed
            return Ok();
        }

    }
}
