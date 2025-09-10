using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ERP.Infrastructure.Repositories
{
    /// <summary>
    /// Implementación del repositorio para manejar la relación UserRole
    /// </summary>
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ErpDbContext _context;

        public UserRoleRepository(ErpDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asignar un rol a un usuario
        /// </summary>
        public async Task<bool> AssignRoleToUserAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default)
        {
            try
            {
                // Verificar si la relación ya existe
                var existingRelation = await _context.UserRoles
                    .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);

                if (existingRelation != null)
                    return false; // Ya existe la relación

                // Crear nueva relación
                var userRole = new UserRole
                {
                    UserId = userId,
                    RoleId = roleId
                };

                _context.UserRoles.Add(userRole);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Remover un rol de un usuario
        /// </summary>
        public async Task<bool> RemoveRoleFromUserAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default)
        {
            try
            {
                var userRole = await _context.UserRoles
                    .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);

                if (userRole == null)
                    return false; // No existe la relación

                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Obtener todos los roles de un usuario
        /// </summary>
        public async Task<List<UserRole>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Obtener todos los usuarios de un rol
        /// </summary>
        public async Task<List<UserRole>> GetRoleUsersAsync(Guid roleId, CancellationToken cancellationToken = default)
        {
            return await _context.UserRoles
                .Where(ur => ur.RoleId == roleId)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Verificar si un usuario tiene un rol específico
        /// </summary>
        public async Task<bool> UserHasRoleAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default)
        {
            return await _context.UserRoles
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);
        }

        /// <summary>
        /// Obtener relaciones UserRole con información de usuarios y roles
        /// </summary>
        public async Task<List<UserRole>> GetUserRolesWithDetailsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.UserRoles
                .Include(ur => ur.Role)
                .Include(ur => ur.User)
                .Where(ur => ur.UserId == userId)
                .ToListAsync(cancellationToken);
        }
    }
}
