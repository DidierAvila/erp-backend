using FluentValidation;
using ERP.Domain.Entities.Inventory;

namespace ERP.Domain.Validators.Inventory
{
    /// <summary>
    /// Validador para la entidad InventoryLocation
    /// </summary>
    public class InventoryLocationValidator : AbstractValidator<InventoryLocation>
    {
        public InventoryLocationValidator()
        {
            RuleFor(x => x.LocationName)
                .NotEmpty().WithMessage("El nombre de la ubicación es requerido")
                .MaximumLength(100).WithMessage("El nombre de la ubicación no puede exceder 100 caracteres")
                .MinimumLength(2).WithMessage("El nombre de la ubicación debe tener al menos 2 caracteres")
                .Matches(@"^[a-zA-Z0-9\s\-_]+$").WithMessage("El nombre de la ubicación solo puede contener letras, números, espacios, guiones y guiones bajos");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("La descripción no puede exceder 500 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}