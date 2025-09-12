using FluentValidation;
using ERP.Domain.Entities.Purchases;

namespace ERP.Domain.Validators.Purchases
{
    /// <summary>
    /// Validador para la entidad PurchaseOrder
    /// </summary>
    public class PurchaseOrderValidator : AbstractValidator<PurchaseOrder>
    {
        public PurchaseOrderValidator()
        {
            RuleFor(x => x.SupplierId)
                .NotEmpty().WithMessage("El ID del proveedor es requerido")
                .GreaterThan(0).WithMessage("El ID del proveedor debe ser mayor a cero");

            RuleFor(x => x.TotalAmount)
                .NotEmpty().WithMessage("El monto total es requerido")
                .GreaterThan(0).WithMessage("El monto total debe ser mayor a cero")
                .PrecisionScale(18, 2, false).WithMessage("El monto total no puede tener más de 2 decimales y debe ser menor a 999,999,999,999,999.99");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("El estado de la orden es requerido")
                .MaximumLength(50).WithMessage("El estado no puede exceder 50 caracteres")
                .Must(status => new[] { "PENDING", "APPROVED", "ORDERED", "RECEIVED", "COMPLETED", "CANCELLED" }.Contains(status.ToUpper()))
                .WithMessage("El estado debe ser PENDING, APPROVED, ORDERED, RECEIVED, COMPLETED o CANCELLED");
        }
    }
}