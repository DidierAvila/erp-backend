using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.Purchases;

public partial class PurchaseOrder
{
    public int Id { get; set; }

    public DateOnly OrderDate { get; set; }

    public int SupplierId { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();

    public virtual Supplier Supplier { get; set; } = null!;
}
