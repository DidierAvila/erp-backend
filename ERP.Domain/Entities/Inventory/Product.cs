using System;
using System.Collections.Generic;
using ERP.Domain.Entities.Sales;
using ERP.Domain.Entities.Purchases;

namespace ERP.Domain.Entities.Inventory;

public partial class Product
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public string Sku { get; set; } = null!;

    public string? Description { get; set; }

    public string UnitOfMeasure { get; set; } = null!;

    public int CurrentStock { get; set; }

    public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();

    public virtual ICollection<SalesOrderItem> SalesOrderItems { get; set; } = new List<SalesOrderItem>();

    public virtual ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
}
