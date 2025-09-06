using System;
using System.Collections.Generic;
using ERP.Domain.Entities.App;

namespace ERP.Domain.Entities.Sales;

public partial class Invoice
{
    public int Id { get; set; }

    public string InvoiceNumber { get; set; } = null!;

    public DateOnly InvoiceDate { get; set; }

    public DateOnly DueDate { get; set; }

    public int? SalesOrderId { get; set; }

    public int CustomerId { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual SalesOrder? SalesOrder { get; set; }
}
