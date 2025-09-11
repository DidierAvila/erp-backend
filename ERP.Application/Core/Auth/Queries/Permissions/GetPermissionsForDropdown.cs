using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Queries.Permissions
{
    public class GetPermissionsForDropdown
    {
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.Permission> _permissionRepository;
        private readonly IMapper _mapper;

        public GetPermissionsForDropdown(IRepositoryBase<ERP.Domain.Entities.Auth.Permission> permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionDropdownDto>> HandleAsync(CancellationToken cancellationToken)
        {
            // Obtener solo los permisos activos, ordenados alfabéticamente
            var activePermissions = await _permissionRepository.Finds(x => x.Status == true, cancellationToken);
            
            // Validar que no sea null y mapear solo Id y Name para máximo rendimiento
            if (activePermissions == null)
                return new List<PermissionDropdownDto>();
            
            return activePermissions
                .OrderBy(p => p.Name)
                .Select(p => new PermissionDropdownDto 
                { 
                    Id = p.Id, 
                    Name = p.Name 
                })
                .ToList();
        }
    }
}
