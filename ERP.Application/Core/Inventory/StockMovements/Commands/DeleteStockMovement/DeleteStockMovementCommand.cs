using ERP.Domain.DTOs.Common;
using MediatR;

namespace ERP.Application.Core.Inventory.StockMovements.Commands.DeleteStockMovement
{
    public class DeleteStockMovementCommand : IRequest<ResponseDto<bool>>
    {
        public int Id { get; set; }
    }
}