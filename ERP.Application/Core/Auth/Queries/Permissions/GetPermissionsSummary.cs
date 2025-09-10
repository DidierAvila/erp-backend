using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Queries.Permissions
{
    public class GetPermissionsSummary
    {
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.Permission> _permissionRepository;
        private readonly IMapper _mapper;

        public GetPermissionsSummary(IRepositoryBase<ERP.Domain.Entities.Auth.Permission> permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionSummaryDto>> HandleAsync(CancellationToken cancellationToken)
        {
            var permissions = await _permissionRepository.GetAll(cancellationToken);
            return _mapper.Map<IEnumerable<PermissionSummaryDto>>(permissions);
        }
    }
}
