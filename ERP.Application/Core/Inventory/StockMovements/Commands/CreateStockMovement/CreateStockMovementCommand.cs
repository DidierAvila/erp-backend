using ERP.Domain.DTOs.Common;
using ERP.Domain.DTOs.Inventory;
using MediatR;

namespace ERP.Application.Core.Inventory.StockMovements.Commands.CreateStockMovement
{
    public class CreateStockMovementCommand : IRequest<ResponseDto<StockMovementDto>>
    {
        public CreateStockMovementDto CreateStockMovementDto { get; set; } = null!;
    }
}