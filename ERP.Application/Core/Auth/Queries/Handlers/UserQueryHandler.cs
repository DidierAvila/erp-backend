using ERP.Application.Core.Auth.Queries.Users;
using ERP.Domain.DTOs.Auth;

namespace ERP.Application.Core.Auth.Queries.Handlers
{
    public class UserQueryHandler : IUserQueryHandler
    {
        private readonly GetUserById _getUserById;
        private readonly GetAllUsers _getAllUsers;

        public UserQueryHandler(GetUserById getUserById, GetAllUsers getAllUsers)
        {
            _getUserById = getUserById;
            _getAllUsers = getAllUsers;
        }

        public async Task<UserDto?> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            return await _getUserById.HandleAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers(CancellationToken cancellationToken)
        {
            return await _getAllUsers.HandleAsync(cancellationToken);
        }
    }
}
