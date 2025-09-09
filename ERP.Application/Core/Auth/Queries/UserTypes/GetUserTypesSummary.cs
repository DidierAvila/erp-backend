using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Queries.UserTypes
{
    public class GetUserTypesSummary
    {
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> _userTypeRepository;
        private readonly IMapper _mapper;

        public GetUserTypesSummary(IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> userTypeRepository, IMapper mapper)
        {
            _userTypeRepository = userTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserTypeSummaryDto>> HandleAsync(CancellationToken cancellationToken)
        {
            var userTypes = await _userTypeRepository.GetAll(cancellationToken);
            return _mapper.Map<IEnumerable<UserTypeSummaryDto>>(userTypes);
        }
    }
}
