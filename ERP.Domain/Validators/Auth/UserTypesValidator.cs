using FluentValidation;
using ERP.Domain.Entities.Auth;

namespace ERP.Domain.Validators.Auth
{
    /// <summary>
    /// Validador para la entidad UserTypes
    /// </summary>
    public class UserTypesValidator : AbstractValidator<UserTypes>
    {
        public UserTypesValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre del tipo de usuario es requerido")
                .MaximumLength(100).WithMessage("El nombre del tipo de usuario no puede exceder 100 caracteres")
                .MinimumLength(2).WithMessage("El nombre del tipo de usuario debe tener al menos 2 caracteres")
                .Matches(@"^[a-zA-Z0-9_\s]+$").WithMessage("El nombre del tipo de usuario solo puede contener letras, números, espacios y guiones bajos");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("La descripción no puede exceder 500 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Status)
                .NotNull().WithMessage("El estado del tipo de usuario es requerido");
        }
    }
}