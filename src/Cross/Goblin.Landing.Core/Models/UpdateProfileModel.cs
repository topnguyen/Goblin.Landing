using Goblin.Identity.Share.Models.UserModels;
using Microsoft.AspNetCore.Http;

namespace Goblin.Landing.Core.Models
{
    public class UpdateProfileModel : GoblinIdentityUpdateProfileModel
    {
        public IFormFile AvatarFile { get; set; }
    }
}