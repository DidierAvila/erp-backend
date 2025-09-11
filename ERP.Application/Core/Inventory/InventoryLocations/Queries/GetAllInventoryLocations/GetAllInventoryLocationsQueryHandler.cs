using AutoMapper;
using ERP.Domain.DTOs.Common;
using ERP.Domain.DTOs.Inventory;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;
using MediatR;

namespace ERP.Application.Core.Inventory.InventoryLocations.Queries.GetAllInventoryLocations
{
    public class GetAllInventoryLocationsQueryHandler : IRequestHandler<GetAllInventoryLocationsQuery, ResponseDto<IEnumerable<InventoryLocationDto>>>
    {
        private readonly IRepositoryBase<InventoryLocation> _repository;
        private readonly IMapper _mapper;

        public GetAllInventoryLocationsQueryHandler(IRepositoryBase<InventoryLocation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<IEnumerable<InventoryLocationDto>>> Handle(GetAllInventoryLocationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var inventoryLocations = await _repository.GetAll(cancellationToken);
                var inventoryLocationDtos = _mapper.Map<IEnumerable<InventoryLocationDto>>(inventoryLocations);

                return new ResponseDto<IEnumerable<InventoryLocationDto>>
                {
                    IsSuccess = true,
                    Message = "Ubicaciones de inventario obtenidas exitosamente",
                    Data = inventoryLocationDtos
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<IEnumerable<InventoryLocationDto>>
                {
                    IsSuccess = false,
                    Message = $"Error al obtener las ubicaciones de inventario: {ex.Message}",
                    Data = null
                };
            }
        }
    }
}