using FluentValidation;
using ERP.Domain.Entities.Inventory;

namespace ERP.Domain.Validators.Inventory
{
    /// <summary>
    /// Validador para la entidad Product
    /// </summary>
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("El nombre del producto es requerido")
                .MaximumLength(200).WithMessage("El nombre del producto no puede exceder 200 caracteres")
                .MinimumLength(2).WithMessage("El nombre del producto debe tener al menos 2 caracteres");

            RuleFor(x => x.Sku)
                .NotEmpty().WithMessage("El SKU es requerido")
                .MaximumLength(50).WithMessage("El SKU no puede exceder 50 caracteres")
                .Matches(@"^[A-Z0-9-_]+$").WithMessage("El SKU solo puede contener letras mayúsculas, números, guiones y guiones bajos");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("La descripción no puede exceder 1000 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.UnitOfMeasure)
                .NotEmpty().WithMessage("La unidad de medida es requerida")
                .MaximumLength(20).WithMessage("La unidad de medida no puede exceder 20 caracteres")
                .Must(unit => new[] { "PCS", "KG", "LT", "MT", "M2", "M3", "BOX", "PAL" }.Contains(unit.ToUpper()))
                .WithMessage("La unidad de medida debe ser una de las siguientes: PCS, KG, LT, MT, M2, M3, BOX, PAL");

            RuleFor(x => x.CurrentStock)
                .GreaterThanOrEqualTo(0).WithMessage("El stock actual no puede ser negativo");
        }
    }
}