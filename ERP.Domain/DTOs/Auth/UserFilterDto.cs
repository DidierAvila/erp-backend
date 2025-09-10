using ERP.Domain.DTOs.Common;
using System.ComponentModel;

namespace ERP.Domain.DTOs.Auth
{
    public class UserFilterDto : PaginationRequestDto
    {
        /// <summary>
        /// Búsqueda general en nombre y email del usuario
        /// </summary>
        public string? Search { get; set; }
        
        /// <summary>
        /// Filtrar por nombre específico del usuario
        /// </summary>
        public string? Name { get; set; }
        
        /// <summary>
        /// Filtrar por email específico del usuario
        /// </summary>
        public string? Email { get; set; }
        
        /// <summary>
        /// Filtrar por ID del rol asignado al usuario
        /// </summary>
        public Guid? RoleId { get; set; }
        
        /// <summary>
        /// Filtrar por ID del tipo de usuario
        /// </summary>
        public Guid? UserTypeId { get; set; }
        
        /// <summary>
        /// Filtrar usuarios creados después de esta fecha
        /// </summary>
        public DateTime? CreatedAfter { get; set; }
        
        /// <summary>
        /// Filtrar usuarios creados antes de esta fecha
        /// </summary>
        public DateTime? CreatedBefore { get; set; }
    }

    public class UserListResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Image { get; set; }
        public string? RoleName { get; set; }
        public string? UserTypeName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
