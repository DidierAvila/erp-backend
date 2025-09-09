using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Commands.UserTypes
{
    public class UpdateUserType
    {
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> _userTypeRepository;
        private readonly IMapper _mapper;

        public UpdateUserType(IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> userTypeRepository, IMapper mapper)
        {
            _userTypeRepository = userTypeRepository;
            _mapper = mapper;
        }

        public async Task<UserTypeDto> HandleAsync(Guid id, UpdateUserTypeDto updateUserTypeDto, CancellationToken cancellationToken)
        {
            // Find existing userType
            var userType = await _userTypeRepository.Find(x => x.Id == id, cancellationToken);
            if (userType == null)
                throw new KeyNotFoundException("UserType not found");

            // Validate that the name doesn't already exist (if it's being updated)
            if (!string.IsNullOrWhiteSpace(updateUserTypeDto.Name) && 
                updateUserTypeDto.Name != userType.Name)
            {
                var existingUserType = await _userTypeRepository.Find(x => x.Name == updateUserTypeDto.Name, cancellationToken);
                if (existingUserType != null)
                    throw new InvalidOperationException("A UserType with this name already exists");
            }

            // Map updated values from DTO to existing entity
            _mapper.Map(updateUserTypeDto, userType);

            // Update the UserType
            await _userTypeRepository.Update(userType, cancellationToken);

            // Map Entity back to DTO for return
            return _mapper.Map<UserTypeDto>(userType);
        }
    }
}
