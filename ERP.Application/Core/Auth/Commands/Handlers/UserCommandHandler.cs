using ERP.Application.Core.Auth.Commands.Users;
using ERP.Domain.DTOs.Auth;

namespace ERP.Application.Core.Auth.Commands.Handlers
{
    public class UserCommandHandler : IUserCommandHandler
    {
        private readonly CreateUser _createUser;
        private readonly UpdateUser _updateUser;
        private readonly DeleteUser _deleteUser;

        public UserCommandHandler(CreateUser createUser, UpdateUser updateUser, DeleteUser deleteUser)
        {
            _createUser = createUser;
            _updateUser = updateUser;
            _deleteUser = deleteUser;
        }

        public async Task<UserDto> CreateUser(CreateUserDto command, CancellationToken cancellationToken)
        {
            return await _createUser.HandleAsync(command, cancellationToken);
        }

        public async Task<UserDto> UpdateUser(Guid id, UpdateUserDto command, CancellationToken cancellationToken)
        {
            return await _updateUser.HandleAsync(id, command, cancellationToken);
        }

        public async Task<bool> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            return await _deleteUser.HandleAsync(id, cancellationToken);
        }
    }
}
