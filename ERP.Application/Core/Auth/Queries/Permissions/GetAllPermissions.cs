using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Queries.Permissions
{
    public class GetAllPermissions
    {
        private readonly IRepositoryBase<Permission> _permissionRepository;
        private readonly IMapper _mapper;

        public GetAllPermissions(IRepositoryBase<Permission> permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionDto>> HandleAsync(CancellationToken cancellationToken)
        {
            var permissions = await _permissionRepository.GetAll(cancellationToken);
            return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
        }
    }
}
