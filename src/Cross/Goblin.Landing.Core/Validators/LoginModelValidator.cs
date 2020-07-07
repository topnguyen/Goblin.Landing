using FluentValidation;
using Goblin.Landing.Core.Models;

namespace Goblin.Landing.Core.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .Length(3, 100)
                .WithMessage("UserName must between 3 and 100 characters");

            RuleFor(x => x.Password)
                .NotEmpty()
                .Length(6, 100)
                .WithMessage("Password must between 6 and 100 characters");
        }   
    }
}