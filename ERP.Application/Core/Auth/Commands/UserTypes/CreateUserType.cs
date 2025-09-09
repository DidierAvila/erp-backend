using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Commands.UserTypes
{
    public class CreateUserType
    {
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> _userTypeRepository;
        private readonly IMapper _mapper;

        public CreateUserType(IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> userTypeRepository, IMapper mapper)
        {
            _userTypeRepository = userTypeRepository;
            _mapper = mapper;
        }

        public async Task<UserTypeDto> HandleAsync(CreateUserTypeDto createUserTypeDto, CancellationToken cancellationToken)
        {
            // Validate that the name doesn't already exist
            var existingUserType = await _userTypeRepository.Find(x => x.Name == createUserTypeDto.Name, cancellationToken);
            if (existingUserType != null)
                throw new InvalidOperationException("A UserType with this name already exists");

            // Map DTO to Entity using AutoMapper
            var userType = _mapper.Map<ERP.Domain.Entities.Auth.UserTypes>(createUserTypeDto);

            // Create the UserType
            await _userTypeRepository.Create(userType, cancellationToken);

            // Map Entity back to DTO for return
            return _mapper.Map<UserTypeDto>(userType);
        }
    }
}
