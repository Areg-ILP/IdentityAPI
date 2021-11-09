using FluentValidation;
using Identity.Infastructure.Application.Commands.IdentityCommands;

namespace Identity.Infastructure.Application.Utilities.Validators
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(model => model.Email)
                .NotEmpty().WithMessage("Email is empty");
            RuleFor(model => model.Password)
               .NotEmpty().WithMessage("Password is empty")
               .Length(8, 20).WithMessage("Lenght of password not valid");
        }
    }
}
