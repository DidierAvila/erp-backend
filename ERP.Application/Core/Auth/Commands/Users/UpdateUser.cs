using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Commands.Users
{
    public class UpdateUser
    {
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IMapper _mapper;

        public UpdateUser(IRepositoryBase<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> HandleAsync(Guid id, UpdateUserDto updateUserDto, CancellationToken cancellationToken)
        {
            // Find existing user
            var user = await _userRepository.Find(x => x.Id == id, cancellationToken);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            // Map DTO properties to existing entity using AutoMapper
            _mapper.Map(updateUserDto, user);
            
            // Ensure UpdatedAt is set
            user.UpdatedAt = DateTime.UtcNow;

            // Update in repository
            await _userRepository.Update(user, cancellationToken);

            // Map Entity to DTO using AutoMapper
            return _mapper.Map<UserDto>(user);
        }
    }
}
