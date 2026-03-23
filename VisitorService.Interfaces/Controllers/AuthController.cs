using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VisitorService.Application.UseCases.Users.Commands;
using VisitorService.Application.UseCases.Users.Commands.CreateManager;
using VisitorService.Interfaces.Extensions;


namespace VisitorService.Interfaces.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterVisitorHandler _registerHandler;
        private readonly IloginHandler _loginHandler;
        private readonly ICreateManagerHandler _createManagerHandler;

        public AuthController(IRegisterVisitorHandler registerHandler, IloginHandler loginHandler, ICreateManagerHandler createManagerHandler)
        {
            _registerHandler = registerHandler;
            _loginHandler = loginHandler;
            _createManagerHandler = createManagerHandler;
        }


        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterVisitorCommand command)
        {
            var result = await _registerHandler.Handle(command);
            if (!result.IsSuccess)
                return BadRequest(new { error = result.Errors });

            return Ok(result.Value);
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

        [HttpPost("Create-manager")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateManager([FromBody] CreateManagerCommand command)
        {
            var managerId = User.GetUserId();

            var result = await _createManagerHandler.Handle(command, managerId);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);

            return StatusCode(201);
        }
    }
}