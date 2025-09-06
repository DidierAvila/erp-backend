using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.Inventory;

public partial class StockMovement
{
    public int Id { get; set; }

    public string MovementType { get; set; } = null!;

    public int ProductId { get; set; }

    public int? FromLocationId { get; set; }

    public int? ToLocationId { get; set; }

    public int Quantity { get; set; }

    public DateTime? MovementDate { get; set; }

    public virtual InventoryLocation? FromLocation { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual InventoryLocation? ToLocation { get; set; }
}
