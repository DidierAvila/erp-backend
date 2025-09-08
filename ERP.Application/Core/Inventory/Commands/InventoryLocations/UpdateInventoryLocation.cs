namespace ERP.Application.Core.Inventory.Commands.InventoryLocations
{
    public class UpdateInventoryLocation
    {
        public int Id { get; set; }
        public string LocationName { get; set; } = string.Empty;
        public string LocationCode { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string LocationType { get; set; } = string.Empty;
        public int? ParentLocationId { get; set; }
    }
}
