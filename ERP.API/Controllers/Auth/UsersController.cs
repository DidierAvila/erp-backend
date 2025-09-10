using ERP.Application.Core.Auth.Commands.Handlers;
using ERP.Application.Core.Auth.Commands.Users;
using ERP.Application.Core.Auth.Queries.Handlers;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.DTOs.Common;
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

        /// <summary>
        /// Obtiene una lista paginada de usuarios con filtros opcionales
        /// </summary>
        /// <param name="filter">Filtros de búsqueda y paginación</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Lista paginada de usuarios</returns>
        /// <remarks>
        /// Campos disponibles para SortBy: name, email, username, createdat, usertypeid
        /// 
        /// Ejemplo de uso:
        /// GET /api/users?page=1&amp;pageSize=10&amp;search=juan&amp;sortBy=name
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<PaginationResponseDto<UserListResponseDto>>> GetAll(
            [FromQuery] UserFilterDto filter, 
            CancellationToken cancellationToken)
        {
            try
            {
                var users = await _userQueryHandler.GetAllUsersFiltered(filter, cancellationToken);
                return Ok(users);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all users with filters");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtener todos los usuarios sin paginación (para uso interno/admin)
        /// Optimizado sin AdditionalData para mejor rendimiento
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserBasicDto>>> GetAllUnpaginated(CancellationToken cancellationToken)
        {
            try
            {
                var users = await _userQueryHandler.GetAllUsersBasic(cancellationToken);
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

        [HttpPut("{id}/change-password")]
        public async Task<ActionResult> ChangePassword(Guid id, [FromBody] ChangePasswordDto changePasswordDto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userCommandHandler.ChangePassword(id, changePasswordDto, cancellationToken);
                if (result)
                    return NoContent();
                
                return BadRequest("Failed to change password");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing password for user with ID {UserId}", id);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Establece un valor en los datos adicionales del usuario
        /// </summary>
        [HttpPut("{id}/additional-data/{key}")]
        public async Task<ActionResult<UserAdditionalValueResponseDto>> SetAdditionalValue(
            Guid id, 
            string key, 
            [FromBody] object value, 
            CancellationToken cancellationToken)
        {
            try
            {
                var operationDto = new UserAdditionalDataOperationDto { Key = key, Value = value };
                var result = await _userCommandHandler.SetUserAdditionalValue(id, operationDto, cancellationToken);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting additional value for user {UserId}, key {Key}", id, key);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene un valor específico de los datos adicionales del usuario
        /// </summary>
        [HttpGet("{id}/additional-data/{key}")]
        public async Task<ActionResult<UserAdditionalValueResponseDto>> GetAdditionalValue(
            Guid id, 
            string key, 
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userCommandHandler.GetUserAdditionalValue(id, key, cancellationToken);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting additional value for user {UserId}, key {Key}", id, key);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un valor de los datos adicionales del usuario
        /// </summary>
        [HttpDelete("{id}/additional-data/{key}")]
        public async Task<ActionResult<bool>> RemoveAdditionalValue(
            Guid id, 
            string key, 
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userCommandHandler.RemoveUserAdditionalValue(id, key, cancellationToken);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing additional value for user {UserId}, key {Key}", id, key);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza todos los datos adicionales del usuario
        /// </summary>
        [HttpPut("{id}/additional-data")]
        public async Task<ActionResult<Dictionary<string, object>>> UpdateAdditionalData(
            Guid id, 
            [FromBody] UpdateUserAdditionalDataDto updateDto, 
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userCommandHandler.UpdateUserAdditionalData(id, updateDto, cancellationToken);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating additional data for user {UserId}", id);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // ======================================
        // ROLE MANAGEMENT ENDPOINTS
        // ======================================

        /// <summary>
        /// Obtiene todos los roles asignados a un usuario
        /// </summary>
        /// <param name="id">ID del usuario</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Lista de roles del usuario</returns>
        [HttpGet("{id}/roles")]
        public async Task<ActionResult<List<UserRoleDto>>> GetUserRoles(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var roles = await _userCommandHandler.GetUserRoles(id, cancellationToken);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting roles for user {UserId}", id);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Asigna múltiples roles a un usuario
        /// </summary>
        /// <param name="id">ID del usuario</param>
        /// <param name="roleIds">Lista de IDs de roles a asignar</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Resultado de la asignación múltiple</returns>
        [HttpPost("{id}/roles")]
        public async Task<ActionResult<MultipleRoleAssignmentResult>> AssignRolesToUser(
            Guid id, 
            [FromBody] List<Guid> roleIds, 
            CancellationToken cancellationToken)
        {
            try
            {
                var command = new ERP.Application.Core.Auth.Commands.Users.AssignMultipleRolesToUser
                {
                    UserId = id,
                    RoleIds = roleIds
                };

                var result = await _userCommandHandler.AssignMultipleRolesToUser(command, cancellationToken);
                
                if (result.IsFullySuccessful)
                {
                    return Ok(result);
                }
                else if (result.AssignedRoles.Count > 0)
                {
                    return Ok(result); // Éxito parcial
                }
                else
                {
                    return BadRequest(result); // Falló todo
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error assigning roles to user {UserId}", id);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Remueve múltiples roles de un usuario
        /// </summary>
        /// <param name="id">ID del usuario</param>
        /// <param name="roleIds">Lista de IDs de roles a remover</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Resultado de la remoción múltiple</returns>
        [HttpDelete("{id}/roles")]
        public async Task<ActionResult<MultipleRoleRemovalResult>> RemoveRolesFromUser(
            Guid id, 
            [FromBody] List<Guid> roleIds, 
            CancellationToken cancellationToken)
        {
            try
            {
                var command = new ERP.Application.Core.Auth.Commands.Users.RemoveMultipleRolesFromUser
                {
                    UserId = id,
                    RoleIds = roleIds
                };

                var result = await _userCommandHandler.RemoveMultipleRolesFromUser(command, cancellationToken);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing roles from user {UserId}", id);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
