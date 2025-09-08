using ERP.Domain.DTOs.Inventory;

namespace ERP.Application.Core.Inventory.Commands.InventoryLocations
{
    public class CreateInventoryLocation
    {
        public string LocationName { get; set; } = string.Empty;
        public string LocationCode { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public string LocationType { get; set; } = "Warehouse"; // Warehouse, Store, Transit, etc.
        public int? ParentLocationId { get; set; } // Para ubicaciones jerárquicas
    }
}
