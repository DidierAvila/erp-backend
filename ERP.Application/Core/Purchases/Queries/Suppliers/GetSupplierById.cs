using AutoMapper;
using ERP.Domain.DTOs.Purchases;
using ERP.Domain.Entities.Purchases;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Purchases.Queries.Suppliers
{
    public class GetSupplierById
    {
        private readonly IRepositoryBase<Supplier> _supplierRepository;
        private readonly IMapper _mapper;

        public GetSupplierById(IRepositoryBase<Supplier> supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public async Task<SupplierDto?> HandleAsync(int id, CancellationToken cancellationToken)
        {
            var supplier = await _supplierRepository.GetByID(id, cancellationToken);
            
            return supplier != null ? _mapper.Map<SupplierDto>(supplier) : null;
        }
    }
}
