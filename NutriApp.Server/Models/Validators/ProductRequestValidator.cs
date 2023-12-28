using FluentValidation;
using NutriApp.Server.Models.Product;

namespace NutriApp.Server.Models.Validators
{
    public class ProductRequestValidator : AbstractValidator<ProductRequest>
    {
        public ProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(256);
            RuleFor(x => x.Brand)
                .MaximumLength(256);
            RuleFor(x => x.Calories)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.Proteins)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.Carbohydrates)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.Fats)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.Ingredients)
                .MaximumLength(512);
            RuleFor(x => x.GramsInPortion)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}