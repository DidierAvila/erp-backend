using ERP.Domain.DTOs.Purchases;

namespace ERP.Application.Core.Purchases.Commands.Handlers
{
    public interface ISupplierCommandHandler
    {
        Task<SupplierDto> CreateSupplier(CreateSupplierDto command, CancellationToken cancellationToken);
        Task<SupplierDto> UpdateSupplier(int id, UpdateSupplierDto command, CancellationToken cancellationToken);
        Task<bool> DeleteSupplier(int id, CancellationToken cancellationToken);
    }
}
