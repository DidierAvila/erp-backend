using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;
using BC = BCrypt.Net.BCrypt;
using Microsoft.Extensions.Logging;

namespace ERP.Application.Core.Auth.Commands.Users
{
    /// <summary>
    /// Servicio para migrar passwords existentes de texto plano a hash BCrypt
    /// Este comando debe ejecutarse una sola vez durante la migración
    /// </summary>
    public class MigratePasswords
    {
        private readonly IRepositoryBase<User> _userRepository;
        private readonly ILogger<MigratePasswords> _logger;

        public MigratePasswords(IRepositoryBase<User> userRepository, ILogger<MigratePasswords> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<int> HandleAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando migración de passwords...");

            var users = await _userRepository.GetAll(cancellationToken);
            int migratedCount = 0;

            foreach (var user in users)
            {
                if (!string.IsNullOrEmpty(user.Password))
                {
                    // Verificar si ya está hasheado (BCrypt hashes empiezan con $2a$, $2b$, etc.)
                    if (!user.Password.StartsWith("$2"))
                    {
                        try
                        {
                            // Hash del password existente
                            var hashedPassword = BC.HashPassword(user.Password, 12);
                            user.Password = hashedPassword;
                            user.UpdatedAt = DateTime.UtcNow;
                            
                            await _userRepository.Update(user, cancellationToken);
                            migratedCount++;
                            
                            _logger.LogInformation("Password migrado para usuario: {Email}", user.Email);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error migrando password para usuario {Email}", user.Email);
                        }
                    }
                    else
                    {
                        _logger.LogInformation("Password ya hasheado para usuario: {Email}", user.Email);
                    }
                }
            }

            _logger.LogInformation("Migración completada. {Count} passwords migrados.", migratedCount);
            return migratedCount;
        }
    }
}
