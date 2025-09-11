using FluentValidation;
using ERP.Domain.Entities.Auth;

namespace ERP.Domain.Validators.Auth
{
    /// <summary>
    /// Validador para la entidad Role
    /// </summary>
    public class RoleValidator : AbstractValidator<Role>
    {
        public RoleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre del rol es requerido")
                .MaximumLength(100).WithMessage("El nombre del rol no puede exceder 100 caracteres")
                .MinimumLength(2).WithMessage("El nombre del rol debe tener al menos 2 caracteres")
                .Matches(@"^[a-zA-Z0-9_\s]+$").WithMessage("El nombre del rol solo puede contener letras, números, espacios y guiones bajos");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("La descripción no puede exceder 500 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Status)
                .NotNull().WithMessage("El estado del rol es requerido");

            RuleFor(x => x.CreatedAt)
                .NotEmpty().WithMessage("La fecha de creación es requerida")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de creación no puede ser futura");

            RuleFor(x => x.UpdatedAt)
                .GreaterThan(x => x.CreatedAt).WithMessage("La fecha de actualización debe ser posterior a la fecha de creación")
                .When(x => x.UpdatedAt.HasValue);
        }
    }
}