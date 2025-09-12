using System.Linq.Expressions;
using ERP.Domain.Repositories;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERP.Infrastructure.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        internal readonly ErpDbContext _context;
        private readonly ILogger<RepositoryBase<TEntity>> _logger;
        
        public RepositoryBase(ErpDbContext context, ILogger<RepositoryBase<TEntity>> logger)
        {
            _context = context;
            _logger = logger;
        }

        internal DbSet<TEntity> EntitySet => _context.Set<TEntity>();

        public async Task<TEntity?> Delete(int id, CancellationToken cancellationToken)
        {
            TEntity? entity = await EntitySet.FindAsync(id, cancellationToken);
            if (entity != null)
            {
                EntitySet.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return entity;
        }

        public async Task<TEntity?> Delete(Guid id, CancellationToken cancellationToken)
        {
            TEntity? entity = await EntitySet.FindAsync(id, cancellationToken);
            if (entity != null)
            {
                EntitySet.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return entity;
        }

        public async Task Delete(TEntity entity, CancellationToken cancellationToken)
        {
            EntitySet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<TEntity?> Find(Expression<Func<TEntity, bool>> expr, CancellationToken cancellationToken)
        {
            try
            {
                return await EntitySet.AsNoTracking().FirstOrDefaultAsync(expr, cancellationToken);
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Database operation was canceled while finding entity of type {EntityType}. This may be due to timeout or connection issues.", typeof(TEntity).Name);
                throw new InvalidOperationException($"La operación de búsqueda fue cancelada. Esto puede deberse a problemas de conexión o timeout de la base de datos.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding entity of type {EntityType}", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<IEnumerable<TEntity>?> Finds(Expression<Func<TEntity, bool>> expr, CancellationToken cancellationToken)
        {
            try
            {
                return await EntitySet.AsNoTracking().Where(expr).ToListAsync(cancellationToken);
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Database operation was canceled while finding multiple entities of type {EntityType}. This may be due to timeout or connection issues.", typeof(TEntity).Name);
                throw new InvalidOperationException($"La operación de búsqueda múltiple fue cancelada. Esto puede deberse a problemas de conexión o timeout de la base de datos.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while finding multiple entities of type {EntityType}", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Database operation was canceled while getting all entities of type {EntityType}. This may be due to timeout or connection issues.", typeof(TEntity).Name);
                throw new InvalidOperationException($"La operación de obtener todas las entidades fue cancelada. Esto puede deberse a problemas de conexión o timeout de la base de datos.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all entities of type {EntityType}", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<TEntity?> GetByID(Guid id, CancellationToken cancellationToken)
        {
            return await EntitySet.FindAsync(id, cancellationToken);
        }

        public async Task<TEntity?> GetByID(int id, CancellationToken cancellationToken)
        {
            return await EntitySet.FindAsync(id, cancellationToken);
        }

        public async Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken)
        {
            var result = await EntitySet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return result.Entity;
        }

        public async Task Update(TEntity entity, CancellationToken cancellationToken)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void GetByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
