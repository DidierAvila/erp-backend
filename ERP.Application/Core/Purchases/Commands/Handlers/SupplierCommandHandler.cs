using ERP.Application.Core.Purchases.Commands.Suppliers;
using ERP.Domain.DTOs.Purchases;

namespace ERP.Application.Core.Purchases.Commands.Handlers
{
    public class SupplierCommandHandler : ISupplierCommandHandler
    {
        private readonly CreateSupplier _createSupplier;
        private readonly UpdateSupplier _updateSupplier;
        private readonly DeleteSupplier _deleteSupplier;

        public SupplierCommandHandler(CreateSupplier createSupplier, UpdateSupplier updateSupplier, DeleteSupplier deleteSupplier)
        {
            _createSupplier = createSupplier;
            _updateSupplier = updateSupplier;
            _deleteSupplier = deleteSupplier;
        }

        public async Task<SupplierDto> CreateSupplier(CreateSupplierDto command, CancellationToken cancellationToken)
        {
            return await _createSupplier.HandleAsync(command, cancellationToken);
        }

        public async Task<SupplierDto> UpdateSupplier(int id, UpdateSupplierDto command, CancellationToken cancellationToken)
        {
            return await _updateSupplier.HandleAsync(id, command, cancellationToken);
        }

        public async Task<bool> DeleteSupplier(int id, CancellationToken cancellationToken)
        {
            return await _deleteSupplier.HandleAsync(id, cancellationToken);
        }
    }
}
