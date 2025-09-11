using ERP.Domain.DTOs.Common;
using ERP.Domain.DTOs.Inventory;
using MediatR;

namespace ERP.Application.Core.Inventory.StockMovements.Commands.UpdateStockMovement
{
    public class UpdateStockMovementCommand : IRequest<ResponseDto<StockMovementDto>>
    {
        public int Id { get; set; }
        public UpdateStockMovementDto UpdateStockMovementDto { get; set; } = null!;
    }
}