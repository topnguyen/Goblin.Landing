using FluentValidation;
using Goblin.Landing.Core.Models;

namespace Goblin.Landing.Core.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(500);

            RuleFor(x => x.Password)
                .NotEmpty()
                .Length(6, 100)
                .WithMessage("Password must between 6 and 100 characters");
        }   
    }
}