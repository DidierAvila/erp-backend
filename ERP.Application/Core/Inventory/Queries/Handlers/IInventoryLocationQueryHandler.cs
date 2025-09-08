using ERP.Application.Core.Inventory.Queries.InventoryLocations;
using ERP.Domain.DTOs.Inventory;

namespace ERP.Application.Core.Inventory.Queries.Handlers
{
    public interface IInventoryLocationQueryHandler
    {
        Task<InventoryLocationDto?> Handle(GetInventoryLocationById query);
        Task<IEnumerable<InventoryLocationDto>> Handle(GetAllInventoryLocations query);
        Task<IEnumerable<InventoryLocationDto>> Handle(GetLocationsByType query);
        Task<IEnumerable<InventoryLocationDto>> Handle(GetLocationsByParent query);
    }
}
