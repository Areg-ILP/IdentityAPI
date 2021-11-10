using FluentValidation;
using Identity.Infastructure.Application.Commands.RoleCommands;

namespace Identity.Infastructure.Application.Utilities.Validators
{
    public class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
    {
        public AddRoleCommandValidator()
        {
            RuleFor(model => model.Name)
                .NotEmpty().WithMessage("Name is empty");
        }
    }
}
