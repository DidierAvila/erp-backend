using System;
using System.Collections.Generic;
using ERP.Domain.Entities.Inventory;

namespace ERP.Domain.Entities.Sales;

public partial class SalesOrderItem
{
    public int Id { get; set; }

    public int SalesOrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual SalesOrder SalesOrder { get; set; } = null!;
}
