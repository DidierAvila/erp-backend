using FluentValidation;
using ERP.Domain.Entities.Auth;

namespace ERP.Domain.Validators.Auth
{
    /// <summary>
    /// Validador para la entidad Session
    /// </summary>
    public class SessionValidator : AbstractValidator<Session>
    {
        public SessionValidator()
        {
            RuleFor(x => x.SessionToken)
                .NotEmpty().WithMessage("El token de sesión es requerido")
                .MinimumLength(10).WithMessage("El token de sesión debe tener al menos 10 caracteres")
                .MaximumLength(500).WithMessage("El token de sesión no puede exceder 500 caracteres");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("El ID del usuario es requerido")
                .When(x => x.UserId.HasValue);

            RuleFor(x => x.Expires)
                .NotEmpty().WithMessage("La fecha de expiración es requerida")
                .GreaterThan(DateTime.Now).WithMessage("La fecha de expiración debe ser futura");
        }
    }
}