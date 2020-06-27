using FluentValidation;
using Goblin.Landing.Core.Models;

namespace Goblin.Landing.Core.Validators
{
    public class CreateSampleModelValidator : AbstractValidator<CreateSampleModel>
    {
        public CreateSampleModelValidator()
        {
            RuleFor(x => x.SampleData)
                .NotEmpty()
                .WithMessage("Please Input Sample Data");
        }
    }
}