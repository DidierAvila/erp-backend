using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Repositories;
using ERP.Domain.Entities.Auth;

namespace ERP.Application.Core.Auth.Queries.Permissions
{
    public class GetActivePermissions
    {
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.Permission> _permissionRepository;
        private readonly IMapper _mapper;

        public GetActivePermissions(IRepositoryBase<ERP.Domain.Entities.Auth.Permission> permissionRepository, IMapper mapper)
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
