using ERP.Application.Core.Auth.Commands.Handlers;
using ERP.Application.Core.Auth.Queries.Handlers;
using ERP.Application.Core.Auth.Queries.RolePermissions;
using ERP.Domain.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionCommandHandler _commandHandler;
        private readonly IPermissionQueryHandler _queryHandler;
        private readonly GetRolesByPermission _getRolesByPermission;

        public PermissionsController(
            IPermissionCommandHandler commandHandler, 
            IPermissionQueryHandler queryHandler,
            GetRolesByPermission getRolesByPermission)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
            _getRolesByPermission = getRolesByPermission;
        }

        /// <summary>
        /// Create a new Permission
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<PermissionDto>> CreatePermission([FromBody] CreatePermissionDto command, CancellationToken cancellationToken)
        {
            try
            {
                var permission = await _commandHandler.CreatePermission(command, cancellationToken);
                return CreatedAtAction(nameof(GetPermissionById), new { id = permission.Id }, permission);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update an existing Permission
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<PermissionDto>> UpdatePermission(Guid id, [FromBody] UpdatePermissionDto command, CancellationToken cancellationToken)
        {
            try
            {
                var permission = await _commandHandler.UpdatePermission(id, command, cancellationToken);
                return Ok(permission);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete a Permission
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeletePermission(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _commandHandler.DeletePermission(id, cancellationToken);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get Permission by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<PermissionDto>> GetPermissionById(Guid id, CancellationToken cancellationToken)
        {
            var permission = await _queryHandler.GetPermissionById(id, cancellationToken);
            if (permission == null)
                return NotFound("Permission not found");

            return Ok(permission);
        }

        /// <summary>
        /// Get all Permissions
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionDto>>> GetAllPermissions(CancellationToken cancellationToken)
        {
            var permissions = await _queryHandler.GetAllPermissions(cancellationToken);
            return Ok(permissions);
        }

        /// <summary>
        /// Get active Permissions only
        /// </summary>
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<PermissionDto>>> GetActivePermissions(CancellationToken cancellationToken)
        {
            var permissions = await _queryHandler.GetActivePermissions(cancellationToken);
            return Ok(permissions);
        }

        /// <summary>
        /// Get Permissions summary with role count
        /// </summary>
        [HttpGet("summary")]
        public async Task<ActionResult<IEnumerable<PermissionSummaryDto>>> GetPermissionsSummary(CancellationToken cancellationToken)
        {
            var permissions = await _queryHandler.GetPermissionsSummary(cancellationToken);
            return Ok(permissions);
        }

        /// <summary>
        /// Obtener todos los roles que tienen un permiso específico
        /// </summary>
        [HttpGet("{permissionId}/roles")]
        public async Task<ActionResult<IEnumerable<RolePermissionDto>>> GetPermissionRoles(Guid permissionId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _getRolesByPermission.HandleAsync(permissionId, cancellationToken);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }
    }
}
