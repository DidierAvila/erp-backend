using ERP.Application.Core.Auth.Commands.Handlers;
using ERP.Application.Helpers;
using ERP.Domain.DTOs.Auth;

namespace ERP.Application.Services
{
    public class PermissionSeederService
    {
        private readonly IPermissionCommandHandler _permissionCommandHandler;

        public PermissionSeederService(IPermissionCommandHandler permissionCommandHandler)
        {
            _permissionCommandHandler = permissionCommandHandler;
        }

        /// <summary>
        /// Inserta todos los permisos definidos en AdminPermissionsHelper a la base de datos
        /// </summary>
        public async Task<PermissionSeederResult> SeedAdminPermissionsAsync(CancellationToken cancellationToken = default)
        {
            var result = new PermissionSeederResult();
            var adminPermissions = AdminPermissionsHelper.GetAdminPermissions();

            foreach (var permissionDto in adminPermissions)
            {
                try
                {
                    // Intentar crear el permiso
                    var createdPermission = await _permissionCommandHandler.CreatePermission(permissionDto, cancellationToken);
                    result.SuccessfulPermissions.Add(createdPermission.Name);
                }
                catch (InvalidOperationException ex) when (ex.Message.Contains("already exists"))
                {
                    // El permiso ya existe, lo agregamos a la lista de existentes
                    result.ExistingPermissions.Add(permissionDto.Name);
                }
                catch (Exception ex)
                {
                    // Error al crear el permiso
                    result.FailedPermissions.Add(new PermissionError 
                    { 
                        PermissionName = permissionDto.Name, 
                        ErrorMessage = ex.Message 
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// Verifica qué permisos ya existen en la base de datos
        /// </summary>
        public async Task<List<string>> GetExistingPermissionNamesAsync(CancellationToken cancellationToken = default)
        {
            var existingPermissions = new List<string>();
            var adminPermissions = AdminPermissionsHelper.GetAdminPermissions();

            foreach (var permission in adminPermissions)
            {
                try
                {
                    // Intentar obtener el permiso por nombre (necesitarías implementar este método)
                    // var existing = await _permissionQueryHandler.GetPermissionByName(permission.Name, cancellationToken);
                    // if (existing != null)
                    // {
                    //     existingPermissions.Add(permission.Name);
                    // }
                }
                catch
                {
                    // Permiso no existe
                }
            }

            return existingPermissions;
        }
    }

    public class PermissionSeederResult
    {
        public List<string> SuccessfulPermissions { get; set; } = new();
        public List<string> ExistingPermissions { get; set; } = new();
        public List<PermissionError> FailedPermissions { get; set; } = new();
        
        public int TotalProcessed => SuccessfulPermissions.Count + ExistingPermissions.Count + FailedPermissions.Count;
        public bool HasErrors => FailedPermissions.Any();
    }

    public class PermissionError
    {
        public string PermissionName { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
