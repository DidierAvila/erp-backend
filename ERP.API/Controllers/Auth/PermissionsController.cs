using ERP.Application.Core.Auth.Commands.Handlers;
using ERP.Application.Core.Auth.Queries.Handlers;
using ERP.Application.Core.Auth.Queries.RolePermissions;
using ERP.Application.Services;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.DTOs.Common;
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
        private readonly PermissionSeederService _permissionSeederService;

        public PermissionsController(
            IPermissionCommandHandler commandHandler, 
            IPermissionQueryHandler queryHandler,
            GetRolesByPermission getRolesByPermission,
            PermissionSeederService permissionSeederService)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
            _getRolesByPermission = getRolesByPermission;
            _permissionSeederService = permissionSeederService;
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
        /// Obtiene una lista paginada de permisos con filtros opcionales
        /// </summary>
        /// <param name="filter">Filtros de búsqueda y paginación</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Lista paginada de permisos</returns>
        /// <remarks>
        /// Campos disponibles para SortBy: name, description, status, createdat
        /// 
        /// Ejemplo de uso:
        /// GET /api/permissions?page=1&amp;pageSize=10&amp;name=read&amp;status=true&amp;sortBy=name
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<PaginationResponseDto<PermissionListResponseDto>>> GetAllPermissions(
            [FromQuery] PermissionFilterDto filter,
            CancellationToken cancellationToken)
        {
            try
            {
                var permissions = await _queryHandler.GetAllPermissionsFiltered(filter, cancellationToken);
                return Ok(permissions);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving permissions");
            }
        }

        /// <summary>
        /// Get all Permissions (simple list without pagination)
        /// </summary>
        [HttpGet("simple")]
        public async Task<ActionResult<IEnumerable<PermissionDto>>> GetAllPermissionsSimple(CancellationToken cancellationToken)
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

        /// <summary>
        /// Ejecuta el seeding de todos los permisos definidos en AdminPermissionsHelper
        /// Este endpoint es para uso administrativo durante la configuración inicial del sistema
        /// </summary>
        [HttpPost("seed-admin-permissions")]
        public async Task<ActionResult<PermissionSeederResult>> SeedAdminPermissions(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _permissionSeederService.SeedAdminPermissionsAsync(cancellationToken);
                
                if (result.HasErrors)
                {
                    return BadRequest(new 
                    { 
                        message = "El seeding se completó con errores", 
                        result = result 
                    });
                }
                
                return Ok(new 
                { 
                    message = "Seeding de permisos completado exitosamente", 
                    result = result 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new 
                { 
                    message = "Error durante el seeding de permisos", 
                    details = ex.Message 
                });
            }
        }
    }
}
