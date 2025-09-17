using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using API.Data.Dtos;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody]CreateUserDto userDto)
        {
            var result = await _service.CreateUserAsync(userDto);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult<UserDto>> DeleteUser(string id)
        {
            var result = await _service.DeleteUserAsync(id);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUser(string email)
        {
            var user = await _service.GetCurrentUser(email);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }
            return Ok(user);
        }

    }
}