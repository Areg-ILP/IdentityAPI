using FluentValidation;
using Identity.Infastructure.Application.Commands.IdentityCommands;

namespace Identity.Infastructure.Application.Utilities.Validators
{
    public class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
    {
        public RegistrationCommandValidator()
        {
            RuleFor(model => model.Email)
               .EmailAddress().WithMessage("Email is empty");
            RuleFor(model => model.Password)
               .NotEmpty().WithMessage("Password is empty")
               .Length(8, 20).WithMessage("Lenght of password not valid");
        }
    }
}
