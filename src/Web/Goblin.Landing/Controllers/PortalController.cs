using Elect.Mapper.AutoMapper.ObjUtils;
using Goblin.Identity.Share.Models.UserModels;
using Goblin.Landing.Core;
using Goblin.Landing.Core.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Goblin.Landing.Controllers
{
    public class PortalController : BaseAuthController
    {
        [Route(Endpoints.Profile)]
        [HttpGet]
        public IActionResult Profile()
        {
            var userProfileModel = LoggedInUser<GoblinIdentityUserModel>.Current.Data.MapTo<GoblinIdentityUpdateProfileModel>();
            
            return View(userProfileModel);
        }  
        
        [Route(Endpoints.Account)]
        [HttpGet]
        public IActionResult Account()
        {
            return View();
        } 
    }
}