using FluentValidation;
using ERP.Domain.Entities.Finance;

namespace ERP.Domain.Validators.Finance
{
    /// <summary>
    /// Validador para la entidad FinancialTransaction
    /// </summary>
    public class FinancialTransactionValidator : AbstractValidator<FinancialTransaction>
    {
        public FinancialTransactionValidator()
        {
            RuleFor(x => x.TransactionType)
                .NotEmpty().WithMessage("El tipo de transacción es requerido")
                .MaximumLength(50).WithMessage("El tipo de transacción no puede exceder 50 caracteres")
                .Must(type => new[] { "DEBIT", "CREDIT", "TRANSFER", "ADJUSTMENT" }.Contains(type.ToUpper()))
                .WithMessage("El tipo de transacción debe ser DEBIT, CREDIT, TRANSFER o ADJUSTMENT");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("El monto es requerido")
                .GreaterThan(0).WithMessage("El monto debe ser mayor a cero")
                .PrecisionScale(18, 2, false).WithMessage("El monto no puede tener más de 2 decimales y debe ser menor a 999,999,999,999,999.99");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("La descripción no puede exceder 500 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.AccountId)
                .NotEmpty().WithMessage("El ID de la cuenta es requerido")
                .GreaterThan(0).WithMessage("El ID de la cuenta debe ser mayor a cero");

            RuleFor(x => x.CreatedAt)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de creación no puede ser futura")
                .When(x => x.CreatedAt.HasValue);
        }
    }
}