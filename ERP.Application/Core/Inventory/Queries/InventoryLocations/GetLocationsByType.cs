namespace ERP.Application.Core.Inventory.Queries.InventoryLocations
{
    public class GetLocationsByType
    {
        public string LocationType { get; set; } = string.Empty;
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 50;
        public bool? IsActive { get; set; } // Filtro opcional por estado
    }
}
