using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Commands.UserTypes
{
    public class DeleteUserType
    {
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> _userTypeRepository;
        private readonly IRepositoryBase<User> _userRepository;

        public DeleteUserType(IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> userTypeRepository, IRepositoryBase<User> userRepository)
        {
            _userTypeRepository = userTypeRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> HandleAsync(Guid id, CancellationToken cancellationToken)
        {
            // Find existing userType
            var userType = await _userTypeRepository.Find(x => x.Id == id, cancellationToken);
            if (userType == null)
                throw new KeyNotFoundException("UserType not found");

            // Check if userType has users
            var hasUsers = await _userRepository.Finds(x => x.UserTypeId == id, cancellationToken);
            if (hasUsers?.Any() == true)
                throw new InvalidOperationException("Cannot delete UserType with associated users. Please reassign users to another type first.");

            // Delete userType
            await _userTypeRepository.Delete(userType, cancellationToken);
            return true;
        }
    }
}
