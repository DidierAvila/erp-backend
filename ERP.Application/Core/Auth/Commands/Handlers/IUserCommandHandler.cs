using ERP.Domain.DTOs.Auth;

namespace ERP.Application.Core.Auth.Commands.Handlers
{
    public interface IUserCommandHandler
    {
        Task<UserDto> CreateUser(CreateUserDto command, CancellationToken cancellationToken);
        Task<UserDto> UpdateUser(Guid id, UpdateUserDto command, CancellationToken cancellationToken);
        Task<bool> DeleteUser(Guid id, CancellationToken cancellationToken);
    }
}