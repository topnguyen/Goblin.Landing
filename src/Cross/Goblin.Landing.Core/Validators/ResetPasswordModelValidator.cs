using FluentValidation;
using Goblin.Landing.Core.Models;

namespace Goblin.Landing.Core.Validators
{
    public class ResetPasswordModelValidator : AbstractValidator<ResetPasswordModel>
    {
        public ResetPasswordModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(500)
                .WithMessage("Please input valid Email");

            RuleFor(x => x.SetPasswordToken)
                .NotEmpty();
            
            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .Length(6, 100)
                .WithMessage("Password must between 6 and 100 characters");
        }   
    }
}