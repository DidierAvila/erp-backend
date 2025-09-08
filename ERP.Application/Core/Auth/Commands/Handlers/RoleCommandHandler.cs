using ERP.Application.Core.Auth.Commands.Roles;
using ERP.Domain.DTOs.Auth;

namespace ERP.Application.Core.Auth.Commands.Handlers
{
    public class RoleCommandHandler : IRoleCommandHandler
    {
        private readonly CreateRole _createRole;
        private readonly UpdateRole _updateRole;
        private readonly DeleteRole _deleteRole;

        public RoleCommandHandler(CreateRole createRole, UpdateRole updateRole, DeleteRole deleteRole)
        {
            _createRole = createRole;
            _updateRole = updateRole;
            _deleteRole = deleteRole;
        }

        public async Task<RoleDto> CreateRole(CreateRoleDto command, CancellationToken cancellationToken)
        {
            return await _createRole.HandleAsync(command, cancellationToken);
        }

        public async Task<RoleDto> UpdateRole(Guid id, UpdateRoleDto command, CancellationToken cancellationToken)
        {
            return await _updateRole.HandleAsync(id, command, cancellationToken);
        }

        public async Task<bool> DeleteRole(Guid id, CancellationToken cancellationToken)
        {
            return await _deleteRole.HandleAsync(id, cancellationToken);
        }
    }
}
