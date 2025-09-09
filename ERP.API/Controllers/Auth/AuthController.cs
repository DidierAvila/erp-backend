using ERP.Application.Core.Auth.Commands.Authentication;
using ERP.Application.Core.Auth.Commands.Handlers;
using ERP.Domain.DTOs;
using ERP.Domain.DTOs.Auth;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly ILoginCommand _loginCommand;
        private readonly IUserCommandHandler _userCommandHandler;
        
        public AuthController(ILoginCommand loginCommand, ILogger<AuthController> logger, IUserCommandHandler userCommandHandler)
        {
            _userCommandHandler = userCommandHandler;
            _loginCommand = loginCommand;
            _logger = logger;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest autorizacion, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Login");
            LoginResponse? Response = await _loginCommand.Login(autorizacion, cancellationToken);
            if (Response == null)
                return BadRequest();

            return Ok(Response.Token);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto autorizacion, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Login");
            var Response = await _userCommandHandler.CreateUser(autorizacion, cancellationToken);
            if (Response == null)
                return BadRequest();

            return Ok(Response);
        }
    }
}
