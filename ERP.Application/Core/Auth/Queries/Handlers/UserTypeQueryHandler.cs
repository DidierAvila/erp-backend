using ERP.Application.Core.Auth.Queries.UserTypes;
using ERP.Application.Core.Auth.Queries.UserType;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.DTOs.Common;

namespace ERP.Application.Core.Auth.Queries.Handlers
{
    public class UserTypeQueryHandler : IUserTypeQueryHandler
    {
        private readonly GetUserTypeById _getUserTypeById;
        private readonly GetAllUserTypes _getAllUserTypes;
        private readonly GetActiveUserTypes _getActiveUserTypes;
        private readonly GetUserTypesSummary _getUserTypesSummary;
        private readonly GetAllUserTypesFiltered _getAllUserTypesFiltered;

        public UserTypeQueryHandler(
            GetUserTypeById getUserTypeById,
            GetAllUserTypes getAllUserTypes,
            GetActiveUserTypes getActiveUserTypes,
            GetUserTypesSummary getUserTypesSummary,
            GetAllUserTypesFiltered getAllUserTypesFiltered)
        {
            _getUserTypeById = getUserTypeById;
            _getAllUserTypes = getAllUserTypes;
            _getActiveUserTypes = getActiveUserTypes;
            _getUserTypesSummary = getUserTypesSummary;
            _getAllUserTypesFiltered = getAllUserTypesFiltered;
        }

        public async Task<UserTypeDto?> GetUserTypeById(Guid id, CancellationToken cancellationToken)
        {
            return await _getUserTypeById.HandleAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<UserTypeDto>> GetAllUserTypes(CancellationToken cancellationToken)
        {
            return await _getAllUserTypes.HandleAsync(cancellationToken);
        }

        public async Task<IEnumerable<UserTypeDto>> GetActiveUserTypes(CancellationToken cancellationToken)
        {
            return await _getActiveUserTypes.HandleAsync(cancellationToken);
        }

        public async Task<IEnumerable<UserTypeSummaryDto>> GetUserTypesSummary(CancellationToken cancellationToken)
        {
            return await _getUserTypesSummary.HandleAsync(cancellationToken);
        }

        public async Task<PaginationResponseDto<UserTypeListResponseDto>> GetAllUserTypesFiltered(UserTypeFilterDto filter, CancellationToken cancellationToken)
        {
            return await _getAllUserTypesFiltered.GetUserTypesFiltered(filter, cancellationToken);
        }
    }
}
