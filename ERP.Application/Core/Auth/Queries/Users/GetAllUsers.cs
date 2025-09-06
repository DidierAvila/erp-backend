using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Queries.Users
{
    public class GetAllUsers
    {
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsers(IRepositoryBase<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> HandleAsync(CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAll(cancellationToken);

            // Map collection of Entities to DTOs using AutoMapper
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}
