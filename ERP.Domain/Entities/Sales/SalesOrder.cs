using System;
using System.Collections.Generic;
using ERP.Domain.Entities.App;

namespace ERP.Domain.Entities.Sales;

public partial class SalesOrder
{
    public int Id { get; set; }

    public DateOnly OrderDate { get; set; }

    public int CustomerId { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<SalesOrderItem> SalesOrderItems { get; set; } = new List<SalesOrderItem>();
}
