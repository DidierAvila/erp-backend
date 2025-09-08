using AutoMapper;
using ERP.Domain.DTOs.Purchases;
using ERP.Domain.Entities.Purchases;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Purchases.Commands.Suppliers
{
    public class UpdateSupplier
    {
        private readonly IRepositoryBase<Supplier> _supplierRepository;
        private readonly IMapper _mapper;

        public UpdateSupplier(IRepositoryBase<Supplier> supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public async Task<SupplierDto> HandleAsync(int id, UpdateSupplierDto updateSupplierDto, CancellationToken cancellationToken)
        {
            // Validations
            if (string.IsNullOrWhiteSpace(updateSupplierDto.SupplierName))
                throw new ArgumentException("Supplier name is required");

            if (string.IsNullOrWhiteSpace(updateSupplierDto.ContactEmail))
                throw new ArgumentException("Contact email is required");

            // Get existing supplier
            var existingSupplier = await _supplierRepository.GetByID(id, cancellationToken);
            if (existingSupplier == null)
                throw new KeyNotFoundException($"Supplier with ID {id} not found");

            // Check if email is already used by another supplier
            var supplierWithEmail = await _supplierRepository.Find(x => x.ContactEmail == updateSupplierDto.ContactEmail && x.Id != id, cancellationToken);
            if (supplierWithEmail != null)
                throw new InvalidOperationException("A supplier with this contact email already exists");

            // Map updates to existing entity (AutoMapper will update only changed fields)
            _mapper.Map(updateSupplierDto, existingSupplier);

            // Update supplier
            await _supplierRepository.Update(existingSupplier, cancellationToken);

            // Return updated supplier as DTO
            return _mapper.Map<SupplierDto>(existingSupplier);
        }
    }
}
