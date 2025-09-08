using AutoMapper;
using ERP.Domain.DTOs.Purchases;
using ERP.Domain.Entities.Purchases;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Purchases.Queries.Suppliers
{
    public class GetAllSuppliers
    {
        private readonly IRepositoryBase<Supplier> _supplierRepository;
        private readonly IMapper _mapper;

        public GetAllSuppliers(IRepositoryBase<Supplier> supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplierDto>> HandleAsync(CancellationToken cancellationToken)
        {
            var suppliers = await _supplierRepository.GetAll(cancellationToken);

            // Map collection of Entities to DTOs using AutoMapper
            return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
        }
    }
}
