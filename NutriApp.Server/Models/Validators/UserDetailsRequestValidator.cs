using FluentValidation;
using NutriApp.Server.Models.User;

namespace NutriApp.Server.Models.Validators
{
    public class UserDetailsRequestValidator : AbstractValidator<UserDetailsRequest>
    {
        public UserDetailsRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.Age)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Height)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Weight)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.NutritionGoal)
                .NotEmpty();
        }
    }
}