using ERP.Domain.DTOs.Common;
using MediatR;

namespace ERP.Application.Core.Inventory.InventoryLocations.Commands.DeleteInventoryLocation
{
    public class DeleteInventoryLocationCommand : IRequest<ResponseDto<bool>>
    {
        public int Id { get; set; }
    }
}