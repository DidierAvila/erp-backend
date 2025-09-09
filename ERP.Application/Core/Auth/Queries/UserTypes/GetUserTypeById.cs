using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Queries.UserTypes
{
    public class GetUserTypeById
    {
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> _userTypeRepository;
        private readonly IMapper _mapper;

        public GetUserTypeById(IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> userTypeRepository, IMapper mapper)
        {
            _userTypeRepository = userTypeRepository;
            _mapper = mapper;
        }

        public async Task<UserTypeDto?> HandleAsync(Guid id, CancellationToken cancellationToken)
        {
            var userType = await _userTypeRepository.GetByID(id, cancellationToken);
            if (userType == null)
                return null;

            // Map Entity to DTO using AutoMapper
            return _mapper.Map<UserTypeDto>(userType);
        }
    }
}
