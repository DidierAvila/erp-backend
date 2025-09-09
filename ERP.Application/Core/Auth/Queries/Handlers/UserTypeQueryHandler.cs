using ERP.Application.Core.Auth.Queries.UserTypes;
using ERP.Domain.DTOs.Auth;

namespace ERP.Application.Core.Auth.Queries.Handlers
{
    public class UserTypeQueryHandler : IUserTypeQueryHandler
    {
        private readonly GetUserTypeById _getUserTypeById;
        private readonly GetAllUserTypes _getAllUserTypes;
        private readonly GetActiveUserTypes _getActiveUserTypes;
        private readonly GetUserTypesSummary _getUserTypesSummary;

        public UserTypeQueryHandler(
            GetUserTypeById getUserTypeById,
            GetAllUserTypes getAllUserTypes,
            GetActiveUserTypes getActiveUserTypes,
            GetUserTypesSummary getUserTypesSummary)
        {
            _getUserTypeById = getUserTypeById;
            _getAllUserTypes = getAllUserTypes;
            _getActiveUserTypes = getActiveUserTypes;
            _getUserTypesSummary = getUserTypesSummary;
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
    }
}
