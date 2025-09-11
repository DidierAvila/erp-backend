using FluentValidation;
using ERP.Domain.Entities.Auth;

namespace ERP.Domain.Validators.Auth
{
    /// <summary>
    /// Validador para la entidad Permission
    /// </summary>
    public class PermissionValidator : AbstractValidator<Permission>
    {
        public PermissionValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre del permiso es requerido")
                .MaximumLength(100).WithMessage("El nombre del permiso no puede exceder 100 caracteres")
                .MinimumLength(3).WithMessage("El nombre del permiso debe tener al menos 3 caracteres")
                .Matches(@"^[a-z_]+\.[a-z_]+$").WithMessage("El nombre del permiso debe seguir el formato 'recurso.accion' (ej: users.read)");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("La descripción no puede exceder 500 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Status)
                .NotNull().WithMessage("El estado del permiso es requerido");

            RuleFor(x => x.CreatedAt)
                .NotEmpty().WithMessage("La fecha de creación es requerida")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de creación no puede ser futura");

            RuleFor(x => x.UpdatedAt)
                .GreaterThan(x => x.CreatedAt).WithMessage("La fecha de actualización debe ser posterior a la fecha de creación")
                .When(x => x.UpdatedAt.HasValue);
        }
    }
}