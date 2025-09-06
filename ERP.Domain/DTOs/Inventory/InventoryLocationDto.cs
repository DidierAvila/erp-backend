namespace ERP.Domain.DTOs.Inventory
{
    public class InventoryLocationDto
    {
        public int Id { get; set; }
        public string LocationName { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class CreateInventoryLocationDto
    {
        public string LocationName { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class UpdateInventoryLocationDto
    {
        public string LocationName { get; set; } = null!;
        public string? Description { get; set; }
    }
}
