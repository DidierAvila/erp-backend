using ERP.Domain.Entities.Auth;

namespace ERP.Domain.Entities.Sales;

public class SalesOrder
{
    public int Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }

    // Add the missing property to fix CS0117  
    public string OrderNumber { get; set; }

    // Assuming these collections exist based on the test file  
    public ICollection<SalesOrderItem> SalesOrderItems { get; set; } = new List<SalesOrderItem>();
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
