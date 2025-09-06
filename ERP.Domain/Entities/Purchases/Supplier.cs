using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.Purchases;

public partial class Supplier
{
    public int Id { get; set; }

    public string SupplierName { get; set; } = null!;

    public string ContactEmail { get; set; } = null!;

    public string? ContactPhone { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
