using FluentValidation;
using ERP.Domain.Entities.Sales;

namespace ERP.Domain.Validators.Sales
{
    /// <summary>
    /// Validador para la entidad SalesOrderItem
    /// </summary>
    public class SalesOrderItemValidator : AbstractValidator<SalesOrderItem>
    {
        public SalesOrderItemValidator()
        {
            RuleFor(x => x.SalesOrderId)
                .NotEmpty().WithMessage("El ID de la orden de venta es requerido")
                .GreaterThan(0).WithMessage("El ID de la orden de venta debe ser mayor a cero");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("El ID del producto es requerido")
                .GreaterThan(0).WithMessage("El ID del producto debe ser mayor a cero");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("La cantidad es requerida")
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a cero")
                .LessThanOrEqualTo(10000).WithMessage("La cantidad no puede exceder 10,000 unidades");

            RuleFor(x => x.UnitPrice)
                .NotEmpty().WithMessage("El precio unitario es requerido")
                .GreaterThan(0).WithMessage("El precio unitario debe ser mayor a cero")
                .PrecisionScale(18, 2, false).WithMessage("El precio unitario no puede tener más de 2 decimales y debe ser menor a 999,999,999,999,999.99");
        }
    }
}