using System.Security.Claims;
using ERP.Application.Core.Auth.Commands.Authentication;
using ERP.Application.Services;
using ERP.Domain.DTOs;
using ERP.Domain.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers.Auth
{
    /// <summary>
    /// Controlador para la autenticación de usuarios.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly ILoginCommand _loginCommand;
        private readonly UserMeService _userMeService;

        /// <summary>
        /// Constructor de la clase AuthController.
        /// </summary>
        /// <param name="loginCommand">Comando para manejar el inicio de sesión.</param>
        /// <param name="logger">Instancia del logger para registrar información.</param>
        /// <param name="userMeService">Servicio para obtener información del usuario autenticado.</param>
        public AuthController(ILoginCommand loginCommand, ILogger<AuthController> logger, UserMeService userMeService)
            => (_loginCommand, _logger, _userMeService) = (loginCommand, logger, userMeService);

        /// <summary>
        /// Inicia sesión en el sistema con las credenciales proporcionadas.
        /// </summary>
        /// <param name="autorizacion">Objeto que contiene las credenciales de inicio de sesión.</param>
        /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
        /// <returns>Un token JWT si las credenciales son válidas; de lo contrario, un error.</returns>
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

        /// <summary>
        /// Obtiene la información del usuario autenticado en formato híbrido con navegación y permisos agrupados.
        /// Formato optimizado para frontends con navegación dinámica y permisos granulares.
        /// </summary>
        [HttpGet]
        [Route("me")]
        [Authorize]
        public async Task<ActionResult<UserMeResponseDto>> GetMe(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("GetMe endpoint called");

                // Obtener el ID del usuario desde el token JWT
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                _logger.LogInformation("User ID claim from token: {UserIdClaim}", userIdClaim);

                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                {
                    _logger.LogWarning("Invalid or missing user ID in token");
                    return Unauthorized(new { message = "Invalid or missing user ID in token" });
                }

                _logger.LogInformation("Parsed user ID: {UserId}", userId);

                // Obtener la información del usuario en formato híbrido
                var userMeHybrid = await _userMeService.GetUserMeAsync(userId, cancellationToken);

                _logger.LogInformation("Successfully retrieved hybrid user info for user ID: {UserId}", userId);
                return Ok(userMeHybrid);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "User not found in database");
                return NotFound(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user information");
                return StatusCode(500, new
                {
                    success = false,
                    message = "Internal server error",
                    error = ex.Message
                });
            }
        }
    }
}
