using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Commands.Roles
{
    public class CreateRole
    {
        private readonly IRepositoryBase<Role> _roleRepository;
        private readonly IMapper _mapper;

        public CreateRole(IRepositoryBase<Role> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<RoleDto> HandleAsync(CreateRoleDto createRoleDto, CancellationToken cancellationToken)
        {
            // Validations
            if (string.IsNullOrWhiteSpace(createRoleDto.Name))
                throw new ArgumentException("Name is required");

            // Check if role already exists
            var existingRole = await _roleRepository.Find(x => x.Name == createRoleDto.Name, cancellationToken);
            if (existingRole != null)
                throw new InvalidOperationException("Role with this name already exists");

            // Map DTO to Entity using AutoMapper
            var role = _mapper.Map<Role>(createRoleDto);

            // Create role in repository
            var createdRole = await _roleRepository.Create(role, cancellationToken);

            // Map Entity to DTO using AutoMapper
            return _mapper.Map<RoleDto>(createdRole);
        }
    }
}
