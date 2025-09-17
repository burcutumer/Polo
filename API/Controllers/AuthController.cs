using API.Data.Dtos;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;

        }

        [HttpPost]
        public async Task<ActionResult<LoginResponseDto>> CheckUserCredentials([FromBody] LoginRequestDto loginRequest)
        {
            var result = await _service.CheckUserCredentials(loginRequest);
            if (result.Error != null)
            {
                return BadRequest(result.Error);
            }
            return Ok(result);
        }
    }
}