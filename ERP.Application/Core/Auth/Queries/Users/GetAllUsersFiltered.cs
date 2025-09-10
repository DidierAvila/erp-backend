using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.DTOs.Common;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Queries.Users
{
    public class GetAllUsersFiltered
    {
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersFiltered(IRepositoryBase<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PaginationResponseDto<UserListResponseDto>> HandleAsync(UserFilterDto filter, CancellationToken cancellationToken)
        {
            // Validar parámetros de paginación
            if (filter.Page <= 0) filter.Page = 1;
            if (filter.PageSize <= 0) filter.PageSize = 10;
            if (filter.PageSize > 100) filter.PageSize = 100;

            // Obtener todos los usuarios
            var allUsers = await _userRepository.GetAll(cancellationToken);
            var query = allUsers.AsQueryable();

            // Aplicar filtros
            query = ApplyFilters(query, filter);

            // Contar total de registros
            var totalRecords = query.Count();

            // Aplicar ordenamiento
            query = ApplySorting(query, filter.SortBy);

            // Aplicar paginación
            var users = query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            // Mapear a DTOs - ajustado para la estructura real de User
            var userDtos = users.Select(u => new UserListResponseDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Phone = u.Phone,
                Address = u.Addres, // Note: hay un typo en la entidad (Addres en lugar de Address)
                Image = u.Image,
                RoleName = u.Roles?.FirstOrDefault()?.Name, // Tomando el primer rol
                UserTypeName = u.UserType?.Name,
                CreatedAt = u.CreatedAt ?? DateTime.MinValue, // Manejo de nullable
                UpdatedAt = u.UpdatedAt
            }).ToList();

            return userDtos.ToPaginatedResult(
                filter.Page, 
                filter.PageSize, 
                totalRecords, 
                filter.SortBy);
        }

        private IQueryable<User> ApplyFilters(IQueryable<User> query, UserFilterDto filter)
        {
            // Filtro por búsqueda general (solo Name y Email ya que Username no existe)
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                var searchTerm = filter.Search.ToLower();
                query = query.Where(u => 
                    u.Name.ToLower().Contains(searchTerm) ||
                    u.Email.ToLower().Contains(searchTerm));
            }

            // Filtro por nombre
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(u => u.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            // Filtro por email
            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                query = query.Where(u => u.Email.ToLower().Contains(filter.Email.ToLower()));
            }

            // Filtro por rol - usando la colección Roles
            if (filter.RoleId.HasValue)
            {
                query = query.Where(u => u.Roles.Any(r => r.Id == filter.RoleId.Value));
            }

            // Filtro por tipo de usuario
            if (filter.UserTypeId.HasValue)
            {
                query = query.Where(u => u.UserTypeId == filter.UserTypeId.Value);
            }

            // Filtro por estado - omitido ya que no existe en la entidad actual
            // if (filter.Status.HasValue)
            // {
            //     query = query.Where(u => u.Status == filter.Status.Value);
            // }

            // Filtro por fecha de creación
            if (filter.CreatedAfter.HasValue && filter.CreatedAfter.HasValue)
            {
                query = query.Where(u => u.CreatedAt >= filter.CreatedAfter.Value);
            }

            if (filter.CreatedBefore.HasValue && filter.CreatedBefore.HasValue)
            {
                query = query.Where(u => u.CreatedAt <= filter.CreatedBefore.Value);
            }

            return query;
        }

        private IQueryable<User> ApplySorting(IQueryable<User> query, string? sortBy)
        {
            if (string.IsNullOrEmpty(sortBy))
            {
                // Ordenamiento por defecto
                return query.OrderBy(u => u.Name);
            }

            return sortBy.ToLower() switch
            {
                "name" => query.OrderBy(u => u.Name),
                "email" => query.OrderBy(u => u.Email),
                "username" => query.OrderBy(u => u.Email), // Usando email como username
                "createdat" => query.OrderBy(u => u.CreatedAt),
                "usertypeid" => query.OrderBy(u => u.UserTypeId),
                _ => query.OrderBy(u => u.Name) // fallback al ordenamiento por defecto
            };
        }
    }
}
