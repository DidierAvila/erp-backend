using FluentValidation;
using ERP.Domain.Entities.Purchases;

namespace ERP.Domain.Validators.Purchases
{
    /// <summary>
    /// Validador para la entidad Supplier
    /// </summary>
    public class SupplierValidator : AbstractValidator<Supplier>
    {
        public SupplierValidator()
        {
            RuleFor(x => x.SupplierName)
                .NotEmpty().WithMessage("El nombre del proveedor es requerido")
                .MaximumLength(200).WithMessage("El nombre del proveedor no puede exceder 200 caracteres")
                .MinimumLength(2).WithMessage("El nombre del proveedor debe tener al menos 2 caracteres");

            RuleFor(x => x.ContactEmail)
                .NotEmpty().WithMessage("El email de contacto es requerido")
                .EmailAddress().WithMessage("El email de contacto debe tener un formato válido")
                .MaximumLength(100).WithMessage("El email de contacto no puede exceder 100 caracteres");

            RuleFor(x => x.ContactPhone)
                .MaximumLength(20).WithMessage("El teléfono de contacto no puede exceder 20 caracteres")
                .Matches(@"^[+]?[0-9\s\-()]+$").WithMessage("El teléfono de contacto debe contener solo números, espacios, guiones, paréntesis y el símbolo +")
                .When(x => !string.IsNullOrEmpty(x.ContactPhone));

            RuleFor(x => x.Address)
                .MaximumLength(500).WithMessage("La dirección no puede exceder 500 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Address));
        }
    }
}