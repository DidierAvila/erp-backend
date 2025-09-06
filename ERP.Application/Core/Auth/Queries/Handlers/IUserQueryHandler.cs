using ERP.Domain.DTOs.Auth;

namespace ERP.Application.Core.Auth.Queries.Handlers
{
    public interface IUserQueryHandler
    {
        Task<UserDto?> GetUserById(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<UserDto>> GetAllUsers(CancellationToken cancellationToken);
    }
}