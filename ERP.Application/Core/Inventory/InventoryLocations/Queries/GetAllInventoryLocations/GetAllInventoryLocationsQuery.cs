using ERP.Domain.DTOs.Common;
using ERP.Domain.DTOs.Inventory;
using MediatR;

namespace ERP.Application.Core.Inventory.InventoryLocations.Queries.GetAllInventoryLocations
{
    public class GetAllInventoryLocationsQuery : IRequest<ResponseDto<IEnumerable<InventoryLocationDto>>>
    {
    }
}