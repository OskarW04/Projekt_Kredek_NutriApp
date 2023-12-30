using FluentValidation;

namespace NutriApp.Server.Models.Validators
{
    public class PaginationParamsValidator : AbstractValidator<PaginationParams>
    {
        private readonly int[] _allowedPageSizes = [5, 10, 15, 30];

        public PaginationParamsValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!_allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be in [{string.Join(",", _allowedPageSizes)}]");
                }
            });
        }
    }
}