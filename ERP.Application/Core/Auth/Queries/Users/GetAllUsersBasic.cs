using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Queries.Users
{
    public class GetAllUsersBasic
    {
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> _userTypeRepository;
        private readonly IMapper _mapper;

        public GetAllUsersBasic(IRepositoryBase<User> userRepository, IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> userTypeRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _userTypeRepository = userTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserBasicDto>> HandleAsync(CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAll(cancellationToken);
            var userTypes = await _userTypeRepository.GetAll(cancellationToken);

            // Map collection of Entities to DTOs using AutoMapper (sin AdditionalData)
            var userDtos = _mapper.Map<IEnumerable<UserBasicDto>>(users);

            // Asignar UserTypeName manualmente
            foreach (var userDto in userDtos)
            {
                var userType = userTypes.FirstOrDefault(ut => ut.Id == userDto.UserTypeId);
                userDto.UserTypeName = userType?.Name;
            }

            return userDtos;
        }
    }
}
