using FluentValidation;
using ERP.Domain.Entities.Sales;

namespace ERP.Domain.Validators.Sales
{
    /// <summary>
    /// Validador para la entidad Invoice
    /// </summary>
    public class InvoiceValidator : AbstractValidator<Invoice>
    {
        public InvoiceValidator()
        {
            RuleFor(x => x.InvoiceNumber)
                .NotEmpty().WithMessage("El número de factura es requerido")
                .MaximumLength(50).WithMessage("El número de factura no puede exceder 50 caracteres")
                .Matches(@"^[A-Z0-9-]+$").WithMessage("El número de factura solo puede contener letras mayúsculas, números y guiones");

            RuleFor(x => x.InvoiceDate)
                .NotEmpty().WithMessage("La fecha de factura es requerida")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage("La fecha de factura no puede ser futura");

            RuleFor(x => x.DueDate)
                .NotEmpty().WithMessage("La fecha de vencimiento es requerida")
                .GreaterThanOrEqualTo(x => x.InvoiceDate).WithMessage("La fecha de vencimiento debe ser igual o posterior a la fecha de factura");

            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("El ID del cliente es requerido");

            RuleFor(x => x.TotalAmount)
                .NotEmpty().WithMessage("El monto total es requerido")
                .GreaterThan(0).WithMessage("El monto total debe ser mayor a cero")
                .PrecisionScale(18, 2, false).WithMessage("El monto total no puede tener más de 2 decimales y debe ser menor a 999,999,999,999,999.99");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("El estado de la factura es requerido")
                .MaximumLength(50).WithMessage("El estado no puede exceder 50 caracteres")
                .Must(status => new[] { "DRAFT", "SENT", "PAID", "OVERDUE", "CANCELLED" }.Contains(status.ToUpper()))
                .WithMessage("El estado debe ser DRAFT, SENT, PAID, OVERDUE o CANCELLED");

            RuleFor(x => x.SalesOrderId)
                .GreaterThan(0).WithMessage("El ID de la orden de venta debe ser mayor a cero")
                .When(x => x.SalesOrderId.HasValue);
        }
    }
}