using FluentValidation;
using Identity.Infastructure.Application.Commands.RoleCommands;

namespace Identity.Infastructure.Application.Utilities.Validators
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(model => model.OldName)
                .NotEmpty().WithMessage("Old name is empty");
            RuleFor(model => model.NewName)
               .NotEmpty().WithMessage("New Name is empty")
               .Length(1,25).WithMessage("Lenght of password not valid");
        }
    }
}
