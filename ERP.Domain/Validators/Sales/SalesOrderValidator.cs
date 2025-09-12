using FluentValidation;
using ERP.Domain.Entities.Sales;

namespace ERP.Domain.Validators.Sales
{
    /// <summary>
    /// Validador para la entidad SalesOrder
    /// </summary>
    public class SalesOrderValidator : AbstractValidator<SalesOrder>
    {
        public SalesOrderValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("El ID del cliente es requerido");

            RuleFor(x => x.TotalAmount)
                .NotEmpty().WithMessage("El monto total es requerido")
                .GreaterThan(0).WithMessage("El monto total debe ser mayor a cero")
                .PrecisionScale(18, 2, false).WithMessage("El monto total no puede tener más de 2 decimales y debe ser menor a 999,999,999,999,999.99");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("El estado de la orden es requerido")
                .MaximumLength(50).WithMessage("El estado no puede exceder 50 caracteres")
                .Must(status => new[] { "PENDING", "CONFIRMED", "PROCESSING", "SHIPPED", "DELIVERED", "CANCELLED" }.Contains(status.ToUpper()))
                .WithMessage("El estado debe ser PENDING, CONFIRMED, PROCESSING, SHIPPED, DELIVERED o CANCELLED");
        }
    }
}