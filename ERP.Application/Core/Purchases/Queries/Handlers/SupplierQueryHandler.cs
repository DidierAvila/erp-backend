using ERP.Application.Core.Purchases.Queries.Suppliers;
using ERP.Domain.DTOs.Purchases;

namespace ERP.Application.Core.Purchases.Queries.Handlers
{
    public class SupplierQueryHandler : ISupplierQueryHandler
    {
        private readonly GetSupplierById _getSupplierById;
        private readonly GetAllSuppliers _getAllSuppliers;

        public SupplierQueryHandler(GetSupplierById getSupplierById, GetAllSuppliers getAllSuppliers)
        {
            _getSupplierById = getSupplierById;
            _getAllSuppliers = getAllSuppliers;
        }

        public async Task<SupplierDto?> GetSupplierById(int id, CancellationToken cancellationToken)
        {
            return await _getSupplierById.HandleAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<SupplierDto>> GetAllSuppliers(CancellationToken cancellationToken)
        {
            return await _getAllSuppliers.HandleAsync(cancellationToken);
        }

        public async Task<IEnumerable<SupplierDto>> GetSuppliersByName(string nameFilter, CancellationToken cancellationToken)
        {
            // Implementación simple por ahora
            return await _getAllSuppliers.HandleAsync(cancellationToken);
        }
    }
}
