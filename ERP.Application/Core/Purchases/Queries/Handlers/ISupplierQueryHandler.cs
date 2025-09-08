using ERP.Domain.DTOs.Purchases;

namespace ERP.Application.Core.Purchases.Queries.Handlers
{
    public interface ISupplierQueryHandler
    {
        Task<SupplierDto?> GetSupplierById(int id, CancellationToken cancellationToken);
        Task<IEnumerable<SupplierDto>> GetAllSuppliers(CancellationToken cancellationToken);
        Task<IEnumerable<SupplierDto>> GetSuppliersByName(string nameFilter, CancellationToken cancellationToken);
    }
}
