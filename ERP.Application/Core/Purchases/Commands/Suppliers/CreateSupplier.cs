using AutoMapper;
using ERP.Domain.DTOs.Purchases;
using ERP.Domain.Entities.Purchases;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Purchases.Commands.Suppliers
{
    public class CreateSupplier
    {
        private readonly IRepositoryBase<Supplier> _supplierRepository;
        private readonly IMapper _mapper;

        public CreateSupplier(IRepositoryBase<Supplier> supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public async Task<SupplierDto> HandleAsync(CreateSupplierDto createSupplierDto, CancellationToken cancellationToken)
        {
            // Validations
            if (string.IsNullOrWhiteSpace(createSupplierDto.SupplierName))
                throw new ArgumentException("Supplier name is required");

            if (string.IsNullOrWhiteSpace(createSupplierDto.ContactEmail))
                throw new ArgumentException("Contact email is required");

            // Check if supplier already exists
            var existingSupplier = await _supplierRepository.Find(x => x.ContactEmail == createSupplierDto.ContactEmail, cancellationToken);
            if (existingSupplier != null)
                throw new InvalidOperationException("Supplier with this email already exists");

            // Map DTO to Entity using AutoMapper
            var supplier = _mapper.Map<Supplier>(createSupplierDto);

            // Create supplier in repository
            var createdSupplier = await _supplierRepository.Create(supplier, cancellationToken);

            // Map Entity to DTO using AutoMapper
            return _mapper.Map<SupplierDto>(createdSupplier);
        }
    }
}
