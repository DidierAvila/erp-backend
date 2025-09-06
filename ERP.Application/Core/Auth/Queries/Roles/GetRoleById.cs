using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Queries.Roles
{
    public class GetRoleById
    {
        private readonly IRepositoryBase<Role> _roleRepository;

        public GetRoleById(IRepositoryBase<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<RoleDto?> HandleAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _roleRepository.GetByID(id, cancellationToken);
            if (user == null)
                return null;

            return new RoleDto
            {
                Id = user.Id,
                Name = user.Name
            };
        }
    }
}
