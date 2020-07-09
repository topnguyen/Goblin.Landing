using System;
using System.Threading;
using Elect.Mapper.AutoMapper.ObjUtils;
using Goblin.Core.Errors;
using Goblin.Identity.Share;
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
        
        [Route(Endpoints.Profile)]
        [HttpPost]
        public IActionResult SubmitUpdateProfile(GoblinIdentityUpdateProfileModel model, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.WarningMessage = Messages.InvalidData;

                return View("Profile", model);
            }
            
            try
            {
                GoblinIdentityHelper
                    .UpdateProfileAsync(LoggedInUser<GoblinIdentityUserModel>.Current.Data.Id, model, 
                        cancellationToken)
                    .ConfigureAwait(true);
                
                ViewBag.SuccessMessage = "Profile Updated Successfully.";
            }
            catch (GoblinException e)
            {
                ViewBag.ErrorMessage = e.ErrorModel.Message;
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
            }
            return View("Profile", model);
        }  
        
        [Route(Endpoints.Account)]
        [HttpGet]
        public IActionResult Account()
        {
            return View();
        } 
    }
}