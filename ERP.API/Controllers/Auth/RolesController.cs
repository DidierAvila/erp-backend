using ERP.Application.Core.Auth.Commands.Handlers;
using ERP.Application.Core.Auth.Queries.Handlers;
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

        public RolesController(IRoleCommandHandler roleCommandHandler, IRoleQueryHandler roleQueryHandler)
        {
            _roleCommandHandler = roleCommandHandler;
            _roleQueryHandler = roleQueryHandler;
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
    }
}
