using ERP.Application.Core.Auth.Commands.Authentication;
using ERP.Application.Core.Auth.Commands.Handlers;
using ERP.Application.Services;
using ERP.Domain.DTOs;
using ERP.Domain.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ERP.API.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly ILoginCommand _loginCommand;
        private readonly IUserCommandHandler _userCommandHandler;
        private readonly UserMeService _userMeService;
        
        public AuthController(ILoginCommand loginCommand, ILogger<AuthController> logger, IUserCommandHandler userCommandHandler, UserMeService userMeService)
        {
            _userCommandHandler = userCommandHandler;
            _loginCommand = loginCommand;
            _logger = logger;
            _userMeService = userMeService;
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

        /// <summary>
        /// Obtiene la información del usuario autenticado y sus permisos
        /// </summary>
        [HttpGet]
        [Route("me")]
        [Authorize]
        public async Task<ActionResult<UserMeDto>> GetMe(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("GetMe endpoint called");

                // Obtener el ID del usuario desde el token JWT
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                _logger.LogInformation($"User ID claim from token: {userIdClaim}");

                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                {
                    _logger.LogWarning("Invalid or missing user ID in token");
                    return Unauthorized(new { message = "Invalid or missing user ID in token" });
                }

                _logger.LogInformation($"Parsed user ID: {userId}");

                // Obtener la información del usuario y sus permisos
                var userMe = await _userMeService.GetUserMeAsync(userId, cancellationToken);
                if (userMe == null)
                {
                    _logger.LogWarning($"User with ID {userId} not found");
                    return NotFound(new { message = "User not found" });
                }

                _logger.LogInformation($"Successfully retrieved user info for: {userMe.Email}");
                return Ok(userMe);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "User not found in database");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user information");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        /// <summary>
        /// Endpoint de prueba para verificar el token JWT - TEMPORAL
        /// </summary>
        [HttpGet]
        [Route("test-token")]
        [Authorize]
        public IActionResult TestToken()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var name = User.FindFirst(ClaimTypes.Name)?.Value;
                
                return Ok(new { 
                    message = "Token is valid",
                    claims = new {
                        userId = userId,
                        email = email,
                        name = name,
                        allClaims = User.Claims.Select(c => new { c.Type, c.Value })
                    }
                });
            }
            catch (Exception ex)
            {
                return Ok(new { message = "Error reading token", error = ex.Message });
            }
        }
    }
}
