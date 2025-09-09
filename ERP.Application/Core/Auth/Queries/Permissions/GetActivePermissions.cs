using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Queries.Permissions
{
    public class GetActivePermissions
    {
        private readonly IRepositoryBase<Permission> _permissionRepository;
        private readonly IMapper _mapper;

        public GetActivePermissions(IRepositoryBase<Permission> permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionDto>> HandleAsync(CancellationToken cancellationToken)
        {
            var activePermissions = await _permissionRepository.Finds(x => x.Status == true, cancellationToken);
            return _mapper.Map<IEnumerable<PermissionDto>>(activePermissions);
        }
    }
}
