using AutoMapper;
using ERP.Domain.DTOs.Common;
using ERP.Domain.DTOs.Inventory;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;
using MediatR;

namespace ERP.Application.Core.Inventory.StockMovements.Commands.CreateStockMovement
{
    public class CreateStockMovementCommandHandler : IRequestHandler<CreateStockMovementCommand, ResponseDto<StockMovementDto>>
    {
        private readonly IRepositoryBase<StockMovement> _repository;
        private readonly IMapper _mapper;

        public CreateStockMovementCommandHandler(IRepositoryBase<StockMovement> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<StockMovementDto>> Handle(CreateStockMovementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var stockMovement = _mapper.Map<StockMovement>(request.CreateStockMovementDto);
                stockMovement.MovementDate = DateTime.UtcNow;
                
                await _repository.Create(stockMovement, cancellationToken);
                
                var stockMovementDto = _mapper.Map<StockMovementDto>(stockMovement);
                
                return ResponseDto<StockMovementDto>.Success(stockMovementDto, "Movimiento de stock creado exitosamente.");
            }
            catch (Exception ex)
            {
                return ResponseDto<StockMovementDto>.Error($"Error al crear el movimiento de stock: {ex.Message}");
            }
        }
    }
}