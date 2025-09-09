using ERP.Application.Core.Auth.Commands.Handlers;
using ERP.Application.Core.Auth.Commands.RolePermissions;
using ERP.Application.Core.Auth.Queries.Handlers;
using ERP.Application.Core.Auth.Queries.RolePermissions;
using ERP.Domain.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleCommandHandler _roleCommandHandler;
        private readonly IRoleQueryHandler _roleQueryHandler;
        private readonly GetPermissionsByRole _getPermissionsByRole;
        private readonly AssignPermissionToRole _assignPermissionToRole;
        private readonly RemovePermissionFromRole _removePermissionFromRole;

        public RolesController(
            IRoleCommandHandler roleCommandHandler, 
            IRoleQueryHandler roleQueryHandler,
            GetPermissionsByRole getPermissionsByRole,
            AssignPermissionToRole assignPermissionToRole,
            RemovePermissionFromRole removePermissionFromRole)
        {
            _roleCommandHandler = roleCommandHandler;
            _roleQueryHandler = roleQueryHandler;
            _getPermissionsByRole = getPermissionsByRole;
            _assignPermissionToRole = assignPermissionToRole;
            _removePermissionFromRole = removePermissionFromRole;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAllRoles(CancellationToken cancellationToken)
        {
            try
            {
                var roles = await _roleQueryHandler.GetAllRoles(cancellationToken);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Roles/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetRoleById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _roleQueryHandler.GetRoleById(id, cancellationToken);
                if (role == null)
                    return NotFound("Role not found");

                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<RoleDto>> CreateRole([FromBody] CreateRoleDto createRoleDto, CancellationToken cancellationToken)
        {
            try
            {
                var createdRole = await _roleCommandHandler.CreateRole(createRoleDto, cancellationToken);
                return CreatedAtAction(nameof(GetRoleById), new { id = createdRole.Id }, createdRole);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Roles/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<RoleDto>> UpdateRole(Guid id, [FromBody] UpdateRoleDto updateRoleDto, CancellationToken cancellationToken)
        {
            try
            {
                var updatedRole = await _roleCommandHandler.UpdateRole(id, updateRoleDto, cancellationToken);
                return Ok(updatedRole);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Roles/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _roleCommandHandler.DeleteRole(id, cancellationToken);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Roles/{id}/permissions
        /// <summary>
        /// Obtener todos los permisos asignados a un rol específico
        /// </summary>
        [HttpGet("{roleId}/permissions")]
        public async Task<ActionResult<IEnumerable<RolePermissionDto>>> GetRolePermissions(Guid roleId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _getPermissionsByRole.HandleAsync(roleId, cancellationToken);
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

        // POST: api/Roles/{roleId}/permissions/{permissionId}
        /// <summary>
        /// Asignar un permiso específico a un rol
        /// </summary>
        [HttpPost("{roleId}/permissions/{permissionId}")]
        public async Task<ActionResult<RolePermissionDto>> AssignPermissionToRole(Guid roleId, Guid permissionId, CancellationToken cancellationToken)
        {
            try
            {
                var request = new AssignPermissionToRoleDto { RoleId = roleId, PermissionId = permissionId };
                var result = await _assignPermissionToRole.HandleAsync(request, cancellationToken);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        // DELETE: api/Roles/{roleId}/permissions/{permissionId}
        /// <summary>
        /// Remover un permiso específico de un rol
        /// </summary>
        [HttpDelete("{roleId}/permissions/{permissionId}")]
        public async Task<ActionResult<bool>> RemovePermissionFromRole(Guid roleId, Guid permissionId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _removePermissionFromRole.HandleAsync(roleId, permissionId, cancellationToken);
                return Ok(new { success = result, message = "Permiso removido del rol correctamente" });
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
