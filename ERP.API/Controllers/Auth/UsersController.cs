using ERP.Application.Core.Auth.Commands.Handlers;
using ERP.Application.Core.Auth.Queries.Handlers;
using ERP.Domain.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers.Auth
{
    [ApiController]
    [Route("api/auth/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserCommandHandler _userCommandHandler;
        private readonly IUserQueryHandler _userQueryHandler;

        public UsersController(IUserCommandHandler userCommandHandler, IUserQueryHandler userQueryHandler, ILogger<UsersController> logger)
        {
            _userCommandHandler = userCommandHandler;
            _userQueryHandler = userQueryHandler;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var users = await _userQueryHandler.GetAllUsers(cancellationToken);
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all users");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userQueryHandler.GetUserById(id, cancellationToken);
                if (user == null)
                    return NotFound($"User with ID {id} not found");

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user with ID {UserId}", id);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto createDto, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userCommandHandler.CreateUser(createDto, cancellationToken);
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> Update(Guid id, [FromBody] UpdateUserDto updateDto, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userCommandHandler.UpdateUser(id, updateDto, cancellationToken);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user with ID {UserId}", id);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userCommandHandler.DeleteUser(id, cancellationToken);
                if (result)
                    return NoContent();
                
                return BadRequest("Failed to delete user");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user with ID {UserId}", id);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
