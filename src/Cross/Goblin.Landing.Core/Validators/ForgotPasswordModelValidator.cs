using FluentValidation;
using Goblin.Landing.Core.Models;

namespace Goblin.Landing.Core.Validators
{
    public class ForgotPasswordModelValidator : AbstractValidator<ForgotPasswordModel>
    {
        public ForgotPasswordModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(500)
                .WithMessage("Please input valid Email");
        }   
    }
}