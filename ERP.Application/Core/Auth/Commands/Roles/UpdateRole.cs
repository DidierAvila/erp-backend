using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Commands.Roles
{
    public class UpdateRole
    {
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.Role> _roleRepository;
        private readonly IMapper _mapper;

        public UpdateRole(IRepositoryBase<ERP.Domain.Entities.Auth.Role> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<RoleDto> HandleAsync(Guid id, UpdateRoleDto updateRoleDto, CancellationToken cancellationToken)
        {
            // Find existing role
            var role = await _roleRepository.Find(x => x.Id == id, cancellationToken);
            if (role == null)
                throw new KeyNotFoundException("Role not found");

            // Check if name already exists (excluding current role)
            if (!string.IsNullOrWhiteSpace(updateRoleDto.Name))
            {
                var existingRole = await _roleRepository.Find(x => x.Name == updateRoleDto.Name && x.Id != id, cancellationToken);
                if (existingRole != null)
                    throw new InvalidOperationException("Role with this name already exists");
            }

            // Map DTO properties to existing entity using AutoMapper
            _mapper.Map(updateRoleDto, role);

            // Update in repository
            await _roleRepository.Update(role, cancellationToken);

            // Map Entity to DTO using AutoMapper
            return _mapper.Map<RoleDto>(role);
        }
    }
}
