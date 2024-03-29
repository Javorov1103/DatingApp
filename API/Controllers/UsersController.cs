﻿using API.Contracts.Services;
using API.Models.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  

    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        #region Get endpoints
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
        #endregion


        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            var success = _usersService.CreateUser(user);

            return Ok(success);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] User user)
        {
            if (id <=0)
            {
                return BadRequest("Please provide valid Id");
            }

            this._usersService.UpdateUser(id,user);

            return Ok();
        }
        
        
    }
}
