using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Queries.Permissions
{
    public class GetPermissionById
    {
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.Permission> _permissionRepository;
        private readonly IMapper _mapper;

        public GetPermissionById(IRepositoryBase<ERP.Domain.Entities.Auth.Permission> permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task<PermissionDto?> HandleAsync(Guid id, CancellationToken cancellationToken)
        {
            var permission = await _permissionRepository.GetByID(id, cancellationToken);
            if (permission == null)
                return null;

            // Map Entity to DTO using AutoMapper
            return _mapper.Map<PermissionDto>(permission);
        }
    }
}
