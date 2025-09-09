using ERP.Application.Core.Auth.Commands.Users;
using ERP.Domain.DTOs.Auth;

namespace ERP.Application.Core.Auth.Commands.Handlers
{
    public class UserCommandHandler : IUserCommandHandler
    {
        private readonly CreateUser _createUser;
        private readonly UpdateUser _updateUser;
        private readonly DeleteUser _deleteUser;
        private readonly ChangePassword _changePassword;
        private readonly UpdateUserAdditionalData _updateUserAdditionalData;

        public UserCommandHandler(
            CreateUser createUser, 
            UpdateUser updateUser, 
            DeleteUser deleteUser, 
            ChangePassword changePassword,
            UpdateUserAdditionalData updateUserAdditionalData)
        {
            _createUser = createUser;
            _updateUser = updateUser;
            _deleteUser = deleteUser;
            _changePassword = changePassword;
            _updateUserAdditionalData = updateUserAdditionalData;
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

        public async Task<bool> ChangePassword(Guid userId, ChangePasswordDto command, CancellationToken cancellationToken)
        {
            return await _changePassword.HandleAsync(userId, command, cancellationToken);
        }

        public async Task<UserAdditionalValueResponseDto> SetUserAdditionalValue(Guid userId, UserAdditionalDataOperationDto operationDto, CancellationToken cancellationToken)
        {
            return await _updateUserAdditionalData.SetAdditionalValue(userId, operationDto, cancellationToken);
        }

        public async Task<UserAdditionalValueResponseDto> GetUserAdditionalValue(Guid userId, string key, CancellationToken cancellationToken)
        {
            return await _updateUserAdditionalData.GetAdditionalValue(userId, key, cancellationToken);
        }

        public async Task<bool> RemoveUserAdditionalValue(Guid userId, string key, CancellationToken cancellationToken)
        {
            return await _updateUserAdditionalData.RemoveAdditionalValue(userId, key, cancellationToken);
        }

        public async Task<Dictionary<string, object>> UpdateUserAdditionalData(Guid userId, UpdateUserAdditionalDataDto updateDto, CancellationToken cancellationToken)
        {
            return await _updateUserAdditionalData.UpdateAllAdditionalData(userId, updateDto, cancellationToken);
        }
    }
}
