﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoreCraftApi.Data;
using ScoreCraftApi.Enities;

namespace ScoreCraftApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetUserCollection")]
        public async Task<ActionResult<List<User>>> GetUserCollection() 
        {
            return Ok(await new UsersBLL(_context).GetUserCollection());
        }

        [HttpGet("GetUser")]
        public async Task<ActionResult<User>> GetUser(Guid RefUser) 
        {
            var user = await new UsersBLL(_context).GetUser(RefUser);

            if(user is null) return NotFound("User not found");
            return Ok(user);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult<User>> Insert(User model) 
        {
            var newUser = await new UsersBLL(_context).Insert(model);

            if (newUser is null) return BadRequest("Oops, something went wrong. Please try again later or contact support.");

            return Ok(newUser);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<User>> Update(User model)
        {
            var newUser = await new UsersBLL(_context).Update(model);

            if (newUser is null) return BadRequest("Oops, something went wrong. Please try again later or contact support.");

            return Ok(newUser);
        }


        [HttpDelete("Delete")]
        public async Task<ActionResult<User>> Delete(Guid RefMember)
        {
            var isUserDeleted = await new UsersBLL(_context).Delete(RefMember);

            if (isUserDeleted is null) return BadRequest("Oops, something went wrong. Please try again later or contact support.");

            return Ok(RefMember);
        }

    }
}
