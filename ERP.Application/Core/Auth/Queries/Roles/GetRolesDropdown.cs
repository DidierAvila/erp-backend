using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Repositories;
using RoleEntity = ERP.Domain.Entities.Auth.Role;

namespace ERP.Application.Core.Auth.Queries.Roles
{
    public class GetRolesDropdown
    {
        private readonly IRepositoryBase<RoleEntity> _roleRepository;
        private readonly IMapper _mapper;

        public GetRolesDropdown(IRepositoryBase<RoleEntity> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDropdownDto>> HandleAsync(CancellationToken cancellationToken)
        {
            // Obtener solo roles activos, ordenados alfabéticamente
            var roles = await _roleRepository.GetAll(cancellationToken);
            
            // Filtrar solo roles activos y ordenar por nombre
            var activeRoles = roles
                .Where(r => r.Status == true)
                .OrderBy(r => r.Name)
                .ToList();

            // Map collection of Entities to DTOs using AutoMapper
            return _mapper.Map<IEnumerable<RoleDropdownDto>>(activeRoles);
        }
    }
}
