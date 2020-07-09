using FluentValidation;
using Goblin.Identity.Share.Validators.UserValidators;
using Goblin.Landing.Core.Models;

namespace Goblin.Landing.Core.Validators
{
    public class UpdateProfileModelValidator : AbstractValidator<UpdateProfileModel>
    {
        public UpdateProfileModelValidator()
        {
            Include(new GoblinIdentityUpdateProfileModelValidator());
        }   
    }
}