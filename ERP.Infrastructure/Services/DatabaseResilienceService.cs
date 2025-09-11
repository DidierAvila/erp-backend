using Microsoft.Extensions.Logging;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ERP.Infrastructure.Services
{
    /// <summary>
    /// Servicio para manejar la resilencia de operaciones de base de datos
    /// </summary>
    public class DatabaseResilienceService
    {
        private readonly ILogger<DatabaseResilienceService> _logger;
        private const int MaxRetryAttempts = 3;
        private readonly TimeSpan _baseDelay = TimeSpan.FromSeconds(1);

        public DatabaseResilienceService(ILogger<DatabaseResilienceService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Ejecuta una operación con política de reintentos
        /// </summary>
        /// <typeparam name="T">Tipo de retorno</typeparam>
        /// <param name="operation">Operación a ejecutar</param>
        /// <param name="operationName">Nombre de la operación para logging</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Resultado de la operación</returns>
        public async Task<T> ExecuteWithRetryAsync<T>(
            Func<Task<T>> operation,
            string operationName,
            CancellationToken cancellationToken = default)
        {
            var attempt = 0;
            while (true)
            {
                try
                {
                    return await operation();
                }
                catch (Exception ex) when (ShouldRetry(ex, attempt))
                {
                    attempt++;
                    var delay = CalculateDelay(attempt);
                    
                    _logger.LogWarning(
                        "Attempt {Attempt} failed for operation {OperationName}. Retrying in {Delay}ms. Error: {Error}",
                        attempt, operationName, delay.TotalMilliseconds, ex.Message);
                    
                    await Task.Delay(delay, cancellationToken);
                }
                catch (TaskCanceledException ex)
                {
                    _logger.LogError(ex, "Operation {OperationName} was canceled after {Attempt} attempts", operationName, attempt);
                    throw new InvalidOperationException($"La operación '{operationName}' fue cancelada. Esto puede deberse a problemas de conexión o timeout de la base de datos.", ex);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Operation {OperationName} failed permanently after {Attempt} attempts", operationName, attempt);
                    throw;
                }
            }
        }

        /// <summary>
        /// Determina si se debe reintentar la operación basado en el tipo de excepción
        /// </summary>
        private static bool ShouldRetry(Exception exception, int attempt)
        {
            if (attempt >= MaxRetryAttempts)
                return false;

            return exception switch
            {
                SqlException sqlEx => IsTransientSqlException(sqlEx),
                TaskCanceledException => true,
                TimeoutException => true,
                InvalidOperationException invalidOpEx when invalidOpEx.Message.Contains("timeout") => true,
                _ => false
            };
        }

        /// <summary>
        /// Determina si una SqlException es transitoria y puede reintentarse
        /// </summary>
        private static bool IsTransientSqlException(SqlException sqlException)
        {
            // Códigos de error SQL Server que indican errores transitorios
            var transientErrorNumbers = new[]
            {
                -2,    // Timeout
                2,     // Timeout
                53,    // Network error
                121,   // Semaphore timeout
                1205,  // Deadlock
                1222,  // Lock request timeout
                8645,  // Memory error
                8651   // Low memory condition
            };

            return transientErrorNumbers.Contains(sqlException.Number);
        }

        /// <summary>
        /// Calcula el delay para el siguiente intento usando backoff exponencial
        /// </summary>
        private TimeSpan CalculateDelay(int attempt)
        {
            var delay = TimeSpan.FromMilliseconds(_baseDelay.TotalMilliseconds * Math.Pow(2, attempt - 1));
            var jitter = TimeSpan.FromMilliseconds(Random.Shared.Next(0, 1000));
            return delay + jitter;
        }
    }
}