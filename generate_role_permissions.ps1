# Script para generar las asignaciones de RolePermissions

# Definir los roles y sus IDs
$roles = @{
    "SuperAdmin" = "AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA"
    "Admin" = "BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"
    "GerenteGeneral" = "CCCCCCCC-CCCC-CCCC-CCCC-CCCCCCCCCCCC"
    "GerenteFinanciero" = "DDDDDDDD-DDDD-DDDD-DDDD-DDDDDDDDDDDD"
    "GerenteVentas" = "EEEEEEEE-EEEE-EEEE-EEEE-EEEEEEEEEEEE"
    "GerenteInventario" = "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF"
    "Contador" = "12121212-1212-1212-1212-121212121212"
    "Vendedor" = "13131313-1313-1313-1313-131313131313"
    "Almacenero" = "14141414-1414-1414-1414-141414141414"
    "Empleado" = "15151515-1515-1515-1515-151515151515"
}

# Obtener todos los permisos de la base de datos
$permissions = sqlcmd -S .\SQLEXPRESS -d ERP -Q "SELECT Id, Name FROM Auth.Permissions WHERE Status = 1 ORDER BY Name" -h -1 -s "," | Where-Object { $_ -match '^[A-F0-9-]+,' }

# Generar el código C# para las asignaciones
$output = @()

# Super Administrador - Todos los permisos
$output += "// Super Administrador - Todos los permisos"
foreach ($permission in $permissions) {
    $parts = $permission.Split(',')
    $permissionId = $parts[0].Trim()
    $permissionName = $parts[1].Trim()
    $output += "{ new Guid(`"$($roles.SuperAdmin)`"), new Guid(`"$permissionId`") }, // $permissionName"
}

# Administrador - Todos excepto system.*
$output += ""
$output += "// Administrador - Todos excepto system.*"
foreach ($permission in $permissions) {
    $parts = $permission.Split(',')
    $permissionId = $parts[0].Trim()
    $permissionName = $parts[1].Trim()
    if (-not $permissionName.StartsWith('system.')) {
        $output += "{ new Guid(`"$($roles.Admin)`"), new Guid(`"$permissionId`") }, // $permissionName"
    }
}

# Gerente General - Gestión completa excepto system.* y algunos permisos críticos
$output += ""
$output += "// Gerente General - Gestión completa excepto system.* y algunos permisos críticos"
$excludeForGG = @('system.', 'users.delete', 'roles.delete')
foreach ($permission in $permissions) {
    $parts = $permission.Split(',')
    $permissionId = $parts[0].Trim()
    $permissionName = $parts[1].Trim()
    $shouldExclude = $false
    foreach ($exclude in $excludeForGG) {
        if ($permissionName.StartsWith($exclude)) {
            $shouldExclude = $true
            break
        }
    }
    if (-not $shouldExclude) {
        $output += "{ new Guid(`"$($roles.GerenteGeneral)`"), new Guid(`"$permissionId`") }, // $permissionName"
    }
}

# Escribir el resultado a un archivo
$output | Out-File -FilePath "role_permissions_output.txt" -Encoding UTF8

Write-Host "Archivo generado: role_permissions_output.txt"
Write-Host "Total de líneas generadas: $($output.Count)"