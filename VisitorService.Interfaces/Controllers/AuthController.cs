using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VisitorService.Application.DTOS;
using VisitorService.Application.Interfaces;

namespace VisitorService.Interfaces.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterVisitorHandler _registerHandler;
        private readonly IloginHandler _loginHandler;

        public AuthController(IRegisterVisitorHandler registerHandler, IloginHandler loginHandler)
        {
            _registerHandler = registerHandler;
            _loginHandler = loginHandler;
        }


        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterVisitorCommand command)
        {
            var result = await _registerHandler.Handle(command);
            if (!result.IsSuccess)
                return BadRequest(new { error = result.Errors });

            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _loginHandler.Handle(command);
            if (!result.IsSuccess)
                return Unauthorized(new { error = result.Errors });

            return Ok(result.Value);
        }
    }
}