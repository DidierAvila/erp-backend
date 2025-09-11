namespace ERP.Domain.Entities.Inventory;

public partial class InventoryLocation
{
    public int Id { get; set; }

    public string LocationName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<StockMovement> StockMovementFromLocations { get; set; } = new List<StockMovement>();

    public virtual ICollection<StockMovement> StockMovementToLocations { get; set; } = new List<StockMovement>();

    public virtual ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
}
