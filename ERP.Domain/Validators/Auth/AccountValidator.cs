using FluentValidation;
using ERP.Domain.Entities.Auth;

namespace ERP.Domain.Validators.Auth
{
    /// <summary>
    /// Validador para la entidad Account
    /// </summary>
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("El tipo de cuenta es requerido")
                .MaximumLength(50).WithMessage("El tipo de cuenta no puede exceder 50 caracteres")
                .Must(type => new[] { "oauth", "email", "credentials" }.Contains(type.ToLower()))
                .WithMessage("El tipo de cuenta debe ser 'oauth', 'email' o 'credentials'");

            RuleFor(x => x.Provider)
                .NotEmpty().WithMessage("El proveedor es requerido")
                .MaximumLength(100).WithMessage("El proveedor no puede exceder 100 caracteres")
                .Matches(@"^[a-zA-Z0-9_-]+$").WithMessage("El proveedor solo puede contener letras, números, guiones y guiones bajos");

            RuleFor(x => x.ProviderAccountId)
                .NotEmpty().WithMessage("El ID de cuenta del proveedor es requerido")
                .MaximumLength(200).WithMessage("El ID de cuenta del proveedor no puede exceder 200 caracteres");

            RuleFor(x => x.RefreshToken)
                .MaximumLength(1000).WithMessage("El refresh token no puede exceder 1000 caracteres")
                .When(x => !string.IsNullOrEmpty(x.RefreshToken));

            RuleFor(x => x.AccessToken)
                .MaximumLength(1000).WithMessage("El access token no puede exceder 1000 caracteres")
                .When(x => !string.IsNullOrEmpty(x.AccessToken));

            RuleFor(x => x.ExpiresAt)
                .GreaterThan(DateTimeOffset.Now.ToUnixTimeSeconds())
                .WithMessage("La fecha de expiración debe ser futura")
                .When(x => x.ExpiresAt.HasValue);

            RuleFor(x => x.IdToken)
                .MaximumLength(2000).WithMessage("El ID token no puede exceder 2000 caracteres")
                .When(x => !string.IsNullOrEmpty(x.IdToken));

            RuleFor(x => x.Scope)
                .MaximumLength(500).WithMessage("El scope no puede exceder 500 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Scope));

            RuleFor(x => x.SessionState)
                .MaximumLength(200).WithMessage("El estado de sesión no puede exceder 200 caracteres")
                .When(x => !string.IsNullOrEmpty(x.SessionState));

            RuleFor(x => x.TokenType)
                .MaximumLength(50).WithMessage("El tipo de token no puede exceder 50 caracteres")
                .Must(tokenType => string.IsNullOrEmpty(tokenType) || new[] { "Bearer", "Basic" }.Contains(tokenType))
                .WithMessage("El tipo de token debe ser 'Bearer' o 'Basic'")
                .When(x => !string.IsNullOrEmpty(x.TokenType));

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("El ID del usuario es requerido")
                .When(x => x.UserId.HasValue);
        }
    }
}