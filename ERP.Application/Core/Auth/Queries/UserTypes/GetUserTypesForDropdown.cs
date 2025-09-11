using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Queries.UserTypes
{
    public class GetUserTypesForDropdown
    {
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> _userTypeRepository;
        private readonly IMapper _mapper;

        public GetUserTypesForDropdown(IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> userTypeRepository, IMapper mapper)
        {
            _userTypeRepository = userTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserTypeDropdownDto>> HandleAsync(CancellationToken cancellationToken)
        {
            // Obtener solo los tipos de usuario activos, ordenados alfabéticamente
            var activeUserTypes = await _userTypeRepository.Finds(x => x.Status == true, cancellationToken);
            
            // Validar que no sea null y mapear solo Id y Name para máximo rendimiento
            if (activeUserTypes == null)
                return new List<UserTypeDropdownDto>();
            
            return activeUserTypes
                .OrderBy(ut => ut.Name)
                .Select(ut => new UserTypeDropdownDto 
                { 
                    Id = ut.Id, 
                    Name = ut.Name 
                })
                .ToList();
        }
    }
}
