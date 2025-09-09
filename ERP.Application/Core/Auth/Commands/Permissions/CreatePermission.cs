using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Commands.Permissions
{
    public class CreatePermission
    {
        private readonly IRepositoryBase<Permission> _permissionRepository;
        private readonly IMapper _mapper;

        public CreatePermission(IRepositoryBase<Permission> permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task<PermissionDto> HandleAsync(CreatePermissionDto createPermissionDto, CancellationToken cancellationToken)
        {
            // Validate that the name doesn't already exist
            var existingPermission = await _permissionRepository.Find(x => x.Name == createPermissionDto.Name, cancellationToken);
            if (existingPermission != null)
                throw new InvalidOperationException("A Permission with this name already exists");

            // Map DTO to Entity using AutoMapper
            var permission = _mapper.Map<Permission>(createPermissionDto);

            // Create the Permission
            await _permissionRepository.Create(permission, cancellationToken);

            // Map Entity back to DTO for return
            return _mapper.Map<PermissionDto>(permission);
        }
    }
}
