using ERP.Domain.DTOs.Common;
using ERP.Domain.DTOs.Inventory;
using MediatR;

namespace ERP.Application.Core.Inventory.StockMovements.Queries.GetStockMovementById
{
    public class GetStockMovementByIdQuery : IRequest<ResponseDto<StockMovementDto>>
    {
        public int Id { get; set; }
    }
}