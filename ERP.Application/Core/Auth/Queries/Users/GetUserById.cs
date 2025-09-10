using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Queries.Users
{
    public class GetUserById
    {
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> _userTypeRepository;
        private readonly IMapper _mapper;

        public GetUserById(IRepositoryBase<User> userRepository, IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> userTypeRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _userTypeRepository = userTypeRepository;
            _mapper = mapper;
        }

        public async Task<UserDto?> HandleAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Find(x => x.Id == id, cancellationToken);
            if (user == null)
                return null;

            // Obtener el UserType por separado
            var userType = await _userTypeRepository.Find(x => x.Id == user.UserTypeId, cancellationToken);
            
            // Map Entity to DTO using AutoMapper
            var userDto = _mapper.Map<UserDto>(user);
            
            // Asignar el nombre del tipo de usuario
            userDto.UserTypeName = userType?.Name;
            
            return userDto;
        }
    }
}
