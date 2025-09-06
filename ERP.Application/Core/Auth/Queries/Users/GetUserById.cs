using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Queries.Users
{
    public class GetUserById
    {
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IMapper _mapper;

        public GetUserById(IRepositoryBase<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto?> HandleAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByID(id, cancellationToken);
            if (user == null)
                return null;

            // Map Entity to DTO using AutoMapper
            return _mapper.Map<UserDto>(user);
        }
    }
}
