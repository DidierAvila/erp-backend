using ERP.Domain.DTOs.Common;
using ERP.Domain.DTOs.Inventory;
using MediatR;

namespace ERP.Application.Core.Inventory.InventoryLocations.Commands.CreateInventoryLocation
{
    public class CreateInventoryLocationCommand : IRequest<ResponseDto<InventoryLocationDto>>
    {
        public CreateInventoryLocationDto CreateInventoryLocationDto { get; set; } = null!;
    }
}