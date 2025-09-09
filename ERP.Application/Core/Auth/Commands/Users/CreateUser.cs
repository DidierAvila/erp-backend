using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;
using BC = BCrypt.Net.BCrypt;

namespace ERP.Application.Core.Auth.Commands.Users
{
    public class CreateUser
    {
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IMapper _mapper;

        public CreateUser(IRepositoryBase<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> HandleAsync(CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            // Validations
            if (string.IsNullOrWhiteSpace(createUserDto.Email))
                throw new ArgumentException("Email is required");

            if (createUserDto.UserTypeId == Guid.Empty)
                throw new ArgumentException("UserTypeId is required");

            // Check if user already exists
            var existingUser = await _userRepository.Find(x => x.Email == createUserDto.Email, cancellationToken);
            if (existingUser != null)
                throw new InvalidOperationException("User with this email already exists");

            // Map DTO to Entity using AutoMapper
            var user = _mapper.Map<User>(createUserDto);

            // Encrypt password before saving
            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = BC.HashPassword(user.Password, 12);
            }

            // Create user in repository
            var createdUser = await _userRepository.Create(user, cancellationToken);

            // Map Entity to DTO using AutoMapper
            return _mapper.Map<UserDto>(createdUser);
        }
    }
}
