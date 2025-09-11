using FluentValidation;
using ERP.Domain.Entities.Finance;

namespace ERP.Domain.Validators.Finance
{
    /// <summary>
    /// Validador para la entidad Account de Finance
    /// </summary>
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(x => x.AccountName)
                .NotEmpty().WithMessage("El nombre de la cuenta es requerido")
                .MaximumLength(200).WithMessage("El nombre de la cuenta no puede exceder 200 caracteres")
                .MinimumLength(3).WithMessage("El nombre de la cuenta debe tener al menos 3 caracteres");

            RuleFor(x => x.AccountNumber)
                .NotEmpty().WithMessage("El número de cuenta es requerido")
                .MaximumLength(50).WithMessage("El número de cuenta no puede exceder 50 caracteres")
                .Matches(@"^[0-9A-Z-]+$").WithMessage("El número de cuenta solo puede contener números, letras mayúsculas y guiones");

            RuleFor(x => x.AccountType)
                .NotEmpty().WithMessage("El tipo de cuenta es requerido")
                .MaximumLength(50).WithMessage("El tipo de cuenta no puede exceder 50 caracteres")
                .Must(type => new[] { "ASSET", "LIABILITY", "EQUITY", "REVENUE", "EXPENSE" }.Contains(type.ToUpper()))
                .WithMessage("El tipo de cuenta debe ser ASSET, LIABILITY, EQUITY, REVENUE o EXPENSE");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("La descripción no puede exceder 500 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Balance)
                .NotNull().WithMessage("El balance es requerido")
                .GreaterThanOrEqualTo(0).WithMessage("El balance no puede ser negativo")
                .When(x => x.AccountType?.ToUpper() == "ASSET" || x.AccountType?.ToUpper() == "REVENUE")
                .WithMessage("Las cuentas de activo y ingresos no pueden tener balance negativo");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("El estado activo es requerido");

            RuleFor(x => x.CreatedAt)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de creación no puede ser futura")
                .When(x => x.CreatedAt.HasValue);

            RuleFor(x => x.UpdatedAt)
                .GreaterThan(x => x.CreatedAt).WithMessage("La fecha de actualización debe ser posterior a la fecha de creación")
                .When(x => x.UpdatedAt.HasValue && x.CreatedAt.HasValue);
        }
    }
}