using FluentValidation;
using Identity.Infastructure.Application.Commands.RoleCommands;

namespace Identity.Infastructure.Application.Utilities.Validators
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(model => model.Id)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Id must greater than 0");
            RuleFor(model => model.Name)
               .NotEmpty().WithMessage("Name is empty")
               .Length(1,25).WithMessage("Lenght of name not valid");
        }
    }
}
