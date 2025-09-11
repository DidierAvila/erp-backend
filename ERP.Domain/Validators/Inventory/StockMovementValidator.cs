using FluentValidation;
using ERP.Domain.Entities.Inventory;

namespace ERP.Domain.Validators.Inventory
{
    /// <summary>
    /// Validador para la entidad StockMovement
    /// </summary>
    public class StockMovementValidator : AbstractValidator<StockMovement>
    {
        public StockMovementValidator()
        {
            RuleFor(x => x.MovementType)
                .NotEmpty().WithMessage("El tipo de movimiento es requerido")
                .MaximumLength(50).WithMessage("El tipo de movimiento no puede exceder 50 caracteres")
                .Must(type => new[] { "IN", "OUT", "TRANSFER", "ADJUSTMENT", "RETURN" }.Contains(type.ToUpper()))
                .WithMessage("El tipo de movimiento debe ser IN, OUT, TRANSFER, ADJUSTMENT o RETURN");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("El ID del producto es requerido")
                .GreaterThan(0).WithMessage("El ID del producto debe ser mayor a cero");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("La cantidad es requerida")
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a cero");

            RuleFor(x => x.MovementDate)
                .NotEmpty().WithMessage("La fecha de movimiento es requerida")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de movimiento no puede ser futura")
                .When(x => x.MovementDate.HasValue);

            RuleFor(x => x.FromLocationId)
                .GreaterThan(0).WithMessage("El ID de ubicación origen debe ser mayor a cero")
                .When(x => x.FromLocationId.HasValue);

            RuleFor(x => x.ToLocationId)
                .GreaterThan(0).WithMessage("El ID de ubicación destino debe ser mayor a cero")
                .When(x => x.ToLocationId.HasValue);

            // Validación específica para movimientos de transferencia
            RuleFor(x => x.FromLocationId)
                .NotEmpty().WithMessage("La ubicación origen es requerida para movimientos de transferencia")
                .When(x => x.MovementType?.ToUpper() == "TRANSFER");

            RuleFor(x => x.ToLocationId)
                .NotEmpty().WithMessage("La ubicación destino es requerida para movimientos de transferencia")
                .When(x => x.MovementType?.ToUpper() == "TRANSFER");

            RuleFor(x => x)
                .Must(x => x.FromLocationId != x.ToLocationId)
                .WithMessage("La ubicación origen y destino no pueden ser la misma")
                .When(x => x.MovementType?.ToUpper() == "TRANSFER" && x.FromLocationId.HasValue && x.ToLocationId.HasValue);
        }
    }
}