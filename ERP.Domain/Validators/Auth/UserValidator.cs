using FluentValidation;
using ERP.Domain.Entities.Auth;

namespace ERP.Domain.Validators.Auth
{
    /// <summary>
    /// Validador para la entidad User
    /// </summary>
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es requerido")
                .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres")
                .MinimumLength(2).WithMessage("El nombre debe tener al menos 2 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es requerido")
                .EmailAddress().WithMessage("El formato del email no es válido")
                .MaximumLength(255).WithMessage("El email no puede exceder 255 caracteres");

            RuleFor(x => x.Password)
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Password));

            RuleFor(x => x.Phone)
                .Matches(@"^[\+]?[1-9][\d]{0,15}$").WithMessage("El formato del teléfono no es válido")
                .When(x => !string.IsNullOrEmpty(x.Phone));

            RuleFor(x => x.Addres)
                .MaximumLength(500).WithMessage("La dirección no puede exceder 500 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Addres));

            RuleFor(x => x.UserTypeId)
                .NotEmpty().WithMessage("El tipo de usuario es requerido");

            RuleFor(x => x.ExtraData)
                .NotEmpty().WithMessage("Los datos extra son requeridos")
                .Must(BeValidJson).WithMessage("Los datos extra deben ser un JSON válido");
        }

        private bool BeValidJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return false;

            try
            {
                System.Text.Json.JsonDocument.Parse(json);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}