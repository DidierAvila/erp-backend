using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.DTOs.Common;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Queries.UserType
{
    public class GetAllUserTypesFiltered
    {
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> _userTypeRepository;
        private readonly IMapper _mapper;

        public GetAllUserTypesFiltered(IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> userTypeRepository, IMapper mapper)
        {
            _userTypeRepository = userTypeRepository;
            _mapper = mapper;
        }

        public async Task<PaginationResponseDto<UserTypeListResponseDto>> GetUserTypesFiltered(UserTypeFilterDto filter, CancellationToken cancellationToken)
        {

            // Validar y establecer valores por defecto
            if (filter.Page <= 0) filter.Page = 1;
            if (filter.PageSize <= 0) filter.PageSize = 10;
            if (filter.PageSize > 100) filter.PageSize = 100;

            // Obtener todos los tipos de usuario
            var allUserTypes = await _userTypeRepository.GetAll(cancellationToken);
            var query = allUserTypes.AsQueryable();

            // Aplicar filtros
            query = ApplyFilters(query, filter);

            // Contar total de registros
            var totalRecords = query.Count();

            // Aplicar ordenamiento
            query = ApplySorting(query, filter.SortBy);

            // Aplicar paginación
            var userTypes = query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            // Mapear a DTOs
            var userTypeDtos = userTypes.Select(ut => new UserTypeListResponseDto
            {
                Id = ut.Id,
                Name = ut.Name,
                Description = ut.Description,
                Status = ut.Status,
                UserCount = ut.Users?.Count ?? 0
            }).ToList();

            return userTypeDtos.ToPaginatedResult(
                filter.Page, 
                filter.PageSize, 
                totalRecords, 
                filter.SortBy);
        }

        private IQueryable<ERP.Domain.Entities.Auth.UserTypes> ApplyFilters(IQueryable<ERP.Domain.Entities.Auth.UserTypes> query, UserTypeFilterDto filter)
        {
            // Filtro por nombre
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(ut => ut.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            // Filtro por estado
            if (filter.Status.HasValue)
            {
                query = query.Where(ut => ut.Status == filter.Status.Value);
            }

            return query;
        }

        private IQueryable<ERP.Domain.Entities.Auth.UserTypes> ApplySorting(IQueryable<ERP.Domain.Entities.Auth.UserTypes> query, string? sortBy)
        {
            if (string.IsNullOrEmpty(sortBy))
            {
                // Ordenamiento por defecto
                return query.OrderBy(ut => ut.Name);
            }

            return sortBy.ToLower() switch
            {
                "name" => query.OrderBy(ut => ut.Name),
                "description" => query.OrderBy(ut => ut.Description),
                "status" => query.OrderBy(ut => ut.Status),
                _ => query.OrderBy(ut => ut.Name) // fallback al ordenamiento por defecto
            };
        }
    }
}
