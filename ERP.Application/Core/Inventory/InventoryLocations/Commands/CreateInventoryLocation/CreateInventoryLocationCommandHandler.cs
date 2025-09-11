using AutoMapper;
using ERP.Domain.DTOs.Common;
using ERP.Domain.DTOs.Inventory;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;
using MediatR;

namespace ERP.Application.Core.Inventory.InventoryLocations.Commands.CreateInventoryLocation
{
    public class CreateInventoryLocationCommandHandler : IRequestHandler<CreateInventoryLocationCommand, ResponseDto<InventoryLocationDto>>
    {
        private readonly IRepositoryBase<InventoryLocation> _repository;
        private readonly IMapper _mapper;

        public CreateInventoryLocationCommandHandler(IRepositoryBase<InventoryLocation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<InventoryLocationDto>> Handle(CreateInventoryLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Input validations
                if (string.IsNullOrWhiteSpace(request.CreateInventoryLocationDto.LocationName))
                {
                    return new ResponseDto<InventoryLocationDto>
                    {
                        IsSuccess = false,
                        Message = "El nombre de la ubicación es requerido",
                        Data = null
                    };
                }

                // Validate location name length
                if (request.CreateInventoryLocationDto.LocationName.Length > 100)
                {
                    return new ResponseDto<InventoryLocationDto>
                    {
                        IsSuccess = false,
                        Message = "El nombre de la ubicación no puede exceder 100 caracteres",
                        Data = null
                    };
                }

                // Validate description length if provided
                if (!string.IsNullOrEmpty(request.CreateInventoryLocationDto.Description) && 
                    request.CreateInventoryLocationDto.Description.Length > 500)
                {
                    return new ResponseDto<InventoryLocationDto>
                    {
                        IsSuccess = false,
                        Message = "La descripción no puede exceder 500 caracteres",
                        Data = null
                    };
                }

                // Validate location name format (no special characters except spaces, hyphens, underscores)
                if (!System.Text.RegularExpressions.Regex.IsMatch(request.CreateInventoryLocationDto.LocationName, @"^[a-zA-Z0-9\s\-_]+$"))
                {
                    return new ResponseDto<InventoryLocationDto>
                    {
                        IsSuccess = false,
                        Message = "El nombre de la ubicación solo puede contener letras, números, espacios, guiones y guiones bajos",
                        Data = null
                    };
                }

                // Validar que no exista una ubicación con el mismo nombre (case insensitive)
                var existingLocation = await _repository.Find(x => x.LocationName.ToLower() == request.CreateInventoryLocationDto.LocationName.ToLower(), cancellationToken);
                if (existingLocation != null)
                {
                    return new ResponseDto<InventoryLocationDto>
                    {
                        IsSuccess = false,
                        Message = "Ya existe una ubicación con este nombre",
                        Data = null
                    };
                }

                var inventoryLocation = _mapper.Map<InventoryLocation>(request.CreateInventoryLocationDto);
                await _repository.Create(inventoryLocation, cancellationToken);
                // SaveAsync no es necesario, Create ya guarda la entidad

                var inventoryLocationDto = _mapper.Map<InventoryLocationDto>(inventoryLocation);

                return new ResponseDto<InventoryLocationDto>
                {
                    IsSuccess = true,
                    Message = "Ubicación de inventario creada exitosamente",
                    Data = inventoryLocationDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<InventoryLocationDto>
                {
                    IsSuccess = false,
                    Message = $"Error al crear la ubicación de inventario: {ex.Message}",
                    Data = null
                };
            }
        }
    }
}