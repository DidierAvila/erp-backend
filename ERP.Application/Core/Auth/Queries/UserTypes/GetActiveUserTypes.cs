using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Queries.UserTypes
{
    public class GetActiveUserTypes
    {
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> _userTypeRepository;
        private readonly IMapper _mapper;

        public GetActiveUserTypes(IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> userTypeRepository, IMapper mapper)
        {
            _userTypeRepository = userTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserTypeDto>> HandleAsync(CancellationToken cancellationToken)
        {
            var activeUserTypes = await _userTypeRepository.Finds(x => x.Status == true, cancellationToken);
            return _mapper.Map<IEnumerable<UserTypeDto>>(activeUserTypes);
        }
    }
}
