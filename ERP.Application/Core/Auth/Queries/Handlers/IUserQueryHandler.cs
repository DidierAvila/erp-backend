using ERP.Domain.DTOs.Auth;
using ERP.Domain.DTOs.Common;

namespace ERP.Application.Core.Auth.Queries.Handlers
{
    public interface IUserQueryHandler
    {
        Task<UserDto?> GetUserById(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<UserDto>> GetAllUsers(CancellationToken cancellationToken);
        Task<IEnumerable<UserBasicDto>> GetAllUsersBasic(CancellationToken cancellationToken);
        Task<PaginationResponseDto<UserListResponseDto>> GetAllUsersFiltered(UserFilterDto filter, CancellationToken cancellationToken);
    }
}