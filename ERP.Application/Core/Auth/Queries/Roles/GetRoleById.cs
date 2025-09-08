using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Queries.Roles
{
    public class GetRoleById
    {
        private readonly IRepositoryBase<Role> _roleRepository;
        private readonly IMapper _mapper;

        public GetRoleById(IRepositoryBase<Role> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<RoleDto?> HandleAsync(Guid id, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByID(id, cancellationToken);
            if (role == null)
                return null;

            // Map Entity to DTO using AutoMapper
            return _mapper.Map<RoleDto>(role);
        }
    }
}
