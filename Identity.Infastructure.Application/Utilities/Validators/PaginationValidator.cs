using FluentValidation;
using Identity.Infastructure.Application.Models;

namespace Identity.Infastructure.Application.Utilities.Validators
{
    public class PaginationValidator : AbstractValidator<PaginationModel>
    {
        public PaginationValidator()
        {
            RuleFor(model => model.PageNumber)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Page number must greater than 0");
            RuleFor(model => model.PageSize)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Page size must greater than 0");
        }
    }
}
