using ERP.Domain.DTOs.Common;
using ERP.Domain.DTOs.Inventory;
using MediatR;

namespace ERP.Application.Core.Inventory.StockMovements.Queries.GetAllStockMovements
{
    public class GetAllStockMovementsQuery : IRequest<ResponseDto<IEnumerable<StockMovementDto>>>
    {
    }
}