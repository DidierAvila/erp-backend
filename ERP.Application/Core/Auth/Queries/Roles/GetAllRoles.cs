using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Queries.Roles
{
    public class GetAllRoles
    {
        private readonly IRepositoryBase<Role> _roleRepository;
        private readonly IMapper _mapper;

        public GetAllRoles(IRepositoryBase<Role> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> HandleAsync(CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetAll(cancellationToken);

            // Map Entities to DTOs using AutoMapper
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }
    }
}
