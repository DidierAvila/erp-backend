using AutoMapper;
using ERP.Domain.DTOs.Common;
using ERP.Domain.DTOs.Inventory;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;
using MediatR;

namespace ERP.Application.Core.Inventory.InventoryLocations.Queries.GetInventoryLocationById
{
    public class GetInventoryLocationByIdQueryHandler : IRequestHandler<GetInventoryLocationByIdQuery, ResponseDto<InventoryLocationDto>>
    {
        private readonly IRepositoryBase<InventoryLocation> _repository;
        private readonly IMapper _mapper;

        public GetInventoryLocationByIdQueryHandler(IRepositoryBase<InventoryLocation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<InventoryLocationDto>> Handle(GetInventoryLocationByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var inventoryLocation = await _repository.GetByID(request.Id, cancellationToken);
                if (inventoryLocation == null)
                {
                    return new ResponseDto<InventoryLocationDto>
                    {
                        IsSuccess = false,
                        Message = "Ubicación de inventario no encontrada",
                        Data = null
                    };
                }

                var inventoryLocationDto = _mapper.Map<InventoryLocationDto>(inventoryLocation);

                return new ResponseDto<InventoryLocationDto>
                {
                    IsSuccess = true,
                    Message = "Ubicación de inventario obtenida exitosamente",
                    Data = inventoryLocationDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<InventoryLocationDto>
                {
                    IsSuccess = false,
                    Message = $"Error al obtener la ubicación de inventario: {ex.Message}",
                    Data = null
                };
            }
        }
    }
}