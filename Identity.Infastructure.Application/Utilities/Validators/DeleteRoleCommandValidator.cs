using FluentValidation;
using Identity.Infastructure.Application.Commands.RoleCommands;

namespace Identity.Infastructure.Application.Utilities.Validators
{
    public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleCommandValidator()
        {
            RuleFor(model => model.Name)
                .NotEmpty().WithMessage("Name is empty");
        }
    }
}
