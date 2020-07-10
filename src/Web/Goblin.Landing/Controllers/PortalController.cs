using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elect.Mapper.AutoMapper.ObjUtils;
using Elect.Web.IUrlHelperUtils;
using Goblin.Core.DateTimeUtils;
using Goblin.Core.Errors;
using Goblin.Identity.Share;
using Goblin.Identity.Share.Models.UserModels;
using Goblin.Landing.Core;
using Goblin.Landing.Core.Constants;
using Goblin.Landing.Core.Models;
using Goblin.Notification.Share;
using Goblin.Notification.Share.Models;
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
        public async Task<IActionResult> SubmitUpdateProfile(UpdateProfileModel model, CancellationToken cancellationToken = default)
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
            var updateIdentityModel = new GoblinIdentityUpdateIdentityModel
            {
                NewUserName = LoggedInUser<GoblinIdentityUserModel>.Current.Data.UserName,
                NewEmail = LoggedInUser<GoblinIdentityUserModel>.Current.Data.Email
            };
            
            return View(updateIdentityModel);
        }
        
        [Route(Endpoints.Account)]
        [HttpPost]
        public async Task<IActionResult> SubmitUpdateAccount(GoblinIdentityUpdateIdentityModel model, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.WarningMessage = Messages.InvalidData;

                return View("Account", model);
            }

            try
            {
                await GoblinIdentityHelper.UpdateIdentityAsync(LoggedInUser<GoblinIdentityUserModel>.Current.Data.Id, model, cancellationToken).ConfigureAwait(true);

                ViewBag.SuccessMessage = "Account Updated Successfully.";

                if (!string.IsNullOrWhiteSpace(model.NewPassword))
                {
                    return View("~/Views/Auth/Login.cshtml", new LoginModel
                    {
                        Continue = Url.AbsoluteAction("Account", "Portal")
                    });
                }
            }
            catch (GoblinException e)
            {
                ViewBag.ErrorMessage = e.ErrorModel.Message;
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
            }
            
            return View("Account", model);
        }
        
        [Route(Endpoints.VerifyEmail)]
        [HttpGet]
        public async Task<IActionResult> VerifyEmail(CancellationToken cancellationToken = default)
        { 
            var updateIdentityModel = new GoblinIdentityUpdateIdentityModel
            {
                NewUserName = LoggedInUser<GoblinIdentityUserModel>.Current.Data.UserName,
                NewEmail = LoggedInUser<GoblinIdentityUserModel>.Current.Data.Email
            };
            
            if (LoggedInUser<GoblinIdentityUserModel>.Current.Data.EmailConfirmedTime.HasValue)
            {
                ViewBag.ErrorMessage = "Your email already verified!";

                return View("Account", updateIdentityModel);
            }
            
            try
            {
                var emailConfirmationModel = await GoblinIdentityHelper.RequestConfirmEmailAsync(LoggedInUser<GoblinIdentityUserModel>.Current.Data.Id, cancellationToken).ConfigureAwait(true);
                
                // Send Email
                
                var confirmEmailMessage = $"Your verify email code is {emailConfirmationModel.EmailConfirmToken}.";

                if (emailConfirmationModel.EmailConfirmTokenExpireTime.HasValue)
                {
                    confirmEmailMessage += $"<br />Code will expire at {emailConfirmationModel.EmailConfirmTokenExpireTime.Value.ToString("f")}";
                }

                var newEmailModel = new GoblinNotificationNewEmailModel
                {
                    ToEmails = new List<string>
                    {
                        LoggedInUser<GoblinIdentityUserModel>.Current.Data.Email
                    },
                    Subject = $"{SystemSetting.Current.ApplicationName} | Verify Your Email",
                    HtmlBody = confirmEmailMessage
                };

                await GoblinNotificationHelper.SendAsync(newEmailModel, cancellationToken);

                ViewBag.WarningMessage = "Please check your email inbox to get the Verify Code.";

                return View();
            }
            catch (GoblinException e)
            {
                ViewBag.ErrorMessage = e.ErrorModel.Message;
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
            }
            
            return View("Account", updateIdentityModel);
        }
        
        [Route(Endpoints.VerifyEmail)]
        [HttpPost]
        public async Task<IActionResult> SubmitVerifyEmail(GoblinIdentityConfirmEmailModel model, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.WarningMessage = Messages.InvalidData;

                return View("VerifyEmail", model);
            }
            
            try
            {
                model.LoggedInUserId = LoggedInUser<GoblinIdentityUserModel>.Current.Data.Id;
                
                await GoblinIdentityHelper.ConfirmEmailAsync(LoggedInUser<GoblinIdentityUserModel>.Current.Data.Id, model, cancellationToken).ConfigureAwait(true);

                ViewBag.SuccessMessage = "Your email is verified.";

                LoggedInUser<GoblinIdentityUserModel>.Current.Data.EmailConfirmedTime = GoblinDateTimeHelper.SystemTimeNow;
            }
            catch (GoblinException e)
            {
                ViewBag.ErrorMessage = e.ErrorModel.Message;
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
            }
            
            var updateIdentityModel = new GoblinIdentityUpdateIdentityModel
            {
                NewUserName = LoggedInUser<GoblinIdentityUserModel>.Current.Data.UserName,
                NewEmail = LoggedInUser<GoblinIdentityUserModel>.Current.Data.Email
            };

            return View("Account", updateIdentityModel);
        }
    }
}