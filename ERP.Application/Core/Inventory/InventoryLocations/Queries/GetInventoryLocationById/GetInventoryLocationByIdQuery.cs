using ERP.Domain.DTOs.Common;
using ERP.Domain.DTOs.Inventory;
using MediatR;

namespace ERP.Application.Core.Inventory.InventoryLocations.Queries.GetInventoryLocationById
{
    public class GetInventoryLocationByIdQuery : IRequest<ResponseDto<InventoryLocationDto>>
    {
        public int Id { get; set; }
    }
}