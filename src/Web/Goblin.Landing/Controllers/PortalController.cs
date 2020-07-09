using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elect.Mapper.AutoMapper.ObjUtils;
using Goblin.Core.DateTimeUtils;
using Goblin.Core.Errors;
using Goblin.Identity.Share;
using Goblin.Identity.Share.Models.UserModels;
using Goblin.Landing.Core;
using Goblin.Landing.Core.Constants;
using Goblin.Landing.Core.Models;
using Goblin.Resource.Share;
using Goblin.Resource.Share.Models;
using Microsoft.AspNetCore.Mvc;

namespace Goblin.Landing.Controllers
{
    public class PortalController : BaseAuthController
    {
        [Route(Endpoints.Profile)]
        [HttpGet]
        public IActionResult Profile()
        {
            var userProfileModel = LoggedInUser<GoblinIdentityUserModel>.Current.Data.MapTo<UpdateProfileModel>();

            return View(userProfileModel);
        }

        [Route(Endpoints.Profile)]
        [HttpPost]
        public async Task<IActionResult> SubmitUpdateProfile(UpdateProfileModel model,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.WarningMessage = Messages.InvalidData;

                return View("Profile", model);
            }

            try
            {
                var goblinIdentityUploadProfileUserModel = model.MapTo<GoblinIdentityUpdateProfileModel>();

                // Upload Avatar File if have
                
                if (model.AvatarFile != null)
                {
                    await using var memoryStream = new MemoryStream();
                    
                    await model.AvatarFile.CopyToAsync(memoryStream, cancellationToken);
                        
                    var fileBytes = memoryStream.ToArray();
                        
                    var base64 = Convert.ToBase64String(fileBytes);

                    var fileName = model.AvatarFile.FileName ?? model.AvatarFile.Name ?? GoblinDateTimeHelper.SystemTimeNow.ToString("ddMMyyHHmmss");
                        
                    var uploadResourceModel = new GoblinResourceUploadFileModel
                    {
                        LoggedInUserId = LoggedInUser<GoblinIdentityUserModel>.Current.Data.Id,
                        Folder = "avatars",
                        Name = $"user-{LoggedInUser<GoblinIdentityUserModel>.Current.Data.Id}-avatar-{fileName}",
                        ContentBase64 = base64,
                        ImageMaxHeightPx = 800,
                        ImageMaxWidthPx = 800,
                        IsEnableCompressImage = true
                    };
                    
                    var fileModel = await GoblinResourceHelper.UploadAsync(uploadResourceModel, cancellationToken).ConfigureAwait(true);

                    goblinIdentityUploadProfileUserModel.AvatarUrl = fileModel.Slug;

                    // Update Model
                    
                    LoggedInUser<GoblinIdentityUserModel>.Current.Data.AvatarUrl = model.AvatarUrl = goblinIdentityUploadProfileUserModel.AvatarUrl;
                    
                    model.AvatarFile = null;
                }

                // Update Profile

                await GoblinIdentityHelper.UpdateProfileAsync(LoggedInUser<GoblinIdentityUserModel>.Current.Data.Id, goblinIdentityUploadProfileUserModel, cancellationToken).ConfigureAwait(true);

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