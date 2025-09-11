using AutoMapper;
using ERP.Domain.DTOs.Common;
using ERP.Domain.DTOs.Inventory;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;
using MediatR;

namespace ERP.Application.Core.Inventory.InventoryLocations.Commands.UpdateInventoryLocation
{
    public class UpdateInventoryLocationCommandHandler : IRequestHandler<UpdateInventoryLocationCommand, ResponseDto<InventoryLocationDto>>
    {
        private readonly IRepositoryBase<InventoryLocation> _repository;
        private readonly IMapper _mapper;

        public UpdateInventoryLocationCommandHandler(IRepositoryBase<InventoryLocation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<InventoryLocationDto>> Handle(UpdateInventoryLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate ID
                if (request.Id <= 0)
                {
                    return new ResponseDto<InventoryLocationDto>
                    {
                        IsSuccess = false,
                        Message = "ID de ubicación inválido",
                        Data = null
                    };
                }

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

                // Input validations
                if (string.IsNullOrWhiteSpace(request.UpdateInventoryLocationDto.LocationName))
                {
                    return new ResponseDto<InventoryLocationDto>
                    {
                        IsSuccess = false,
                        Message = "El nombre de la ubicación es requerido",
                        Data = null
                    };
                }

                // Validate location name length
                if (request.UpdateInventoryLocationDto.LocationName.Length > 100)
                {
                    return new ResponseDto<InventoryLocationDto>
                    {
                        IsSuccess = false,
                        Message = "El nombre de la ubicación no puede exceder 100 caracteres",
                        Data = null
                    };
                }

                // Validate description length if provided
                if (!string.IsNullOrEmpty(request.UpdateInventoryLocationDto.Description) && 
                    request.UpdateInventoryLocationDto.Description.Length > 500)
                {
                    return new ResponseDto<InventoryLocationDto>
                    {
                        IsSuccess = false,
                        Message = "La descripción no puede exceder 500 caracteres",
                        Data = null
                    };
                }

                // Validate location name format
                if (!System.Text.RegularExpressions.Regex.IsMatch(request.UpdateInventoryLocationDto.LocationName, @"^[a-zA-Z0-9\s\-_]+$"))
                {
                    return new ResponseDto<InventoryLocationDto>
                    {
                        IsSuccess = false,
                        Message = "El nombre de la ubicación solo puede contener letras, números, espacios, guiones y guiones bajos",
                        Data = null
                    };
                }

                // Validar que no exista otra ubicación con el mismo nombre (case insensitive)
                var existingLocation = await _repository.Find(x => x.LocationName.ToLower() == request.UpdateInventoryLocationDto.LocationName.ToLower() && x.Id != request.Id, cancellationToken);
                if (existingLocation != null)
                {
                    return new ResponseDto<InventoryLocationDto>
                    {
                        IsSuccess = false,
                        Message = "Ya existe otra ubicación con este nombre",
                        Data = null
                    };
                }

                _mapper.Map(request.UpdateInventoryLocationDto, inventoryLocation);
                await _repository.Update(inventoryLocation, cancellationToken);

                var inventoryLocationDto = _mapper.Map<InventoryLocationDto>(inventoryLocation);

                return new ResponseDto<InventoryLocationDto>
                {
                    IsSuccess = true,
                    Message = "Ubicación de inventario actualizada exitosamente",
                    Data = inventoryLocationDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<InventoryLocationDto>
                {
                    IsSuccess = false,
                    Message = $"Error al actualizar la ubicación de inventario: {ex.Message}",
                    Data = null
                };
            }
        }
    }
}