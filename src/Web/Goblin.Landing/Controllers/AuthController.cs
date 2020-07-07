using System;
using System.Threading;
using System.Threading.Tasks;
using Elect.Web.IUrlHelperUtils;
using Goblin.Core.Constants;
using Goblin.Core.Utils;
using Goblin.Identity.Share;
using Goblin.Identity.Share.Models.UserModels;
using Goblin.Landing.Core.Constants;
using Goblin.Landing.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Goblin.Landing.Controllers
{
    public class AuthController : BaseController
    {
        
        [Route(Endpoints.Login)]
        [HttpGet]
        public IActionResult Login(string @continue)
        {
            var loginModel = new LoginModel
            {
                Continue = @continue
            };
            
            return View(loginModel);
        }   
        
        [Route(Endpoints.Login)]
        [HttpPost]
        public async Task<IActionResult> SubmitLogin([FromForm] LoginModel model, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }
            
            if (string.IsNullOrWhiteSpace(model.Continue))
            {
                model.Continue = Url.AbsoluteAction("Index", "Home");
            }

            ViewBag.ContinueUrl = model.Continue;
            
            try
            {
                var generateAccessTokenModel = new GoblinIdentityGenerateAccessTokenModel
                {
                    UserName = model.UserName,
                    Password = model.Password
                };
                
                var accessToken = await GoblinIdentityHelper.GenerateAccessTokenAsync(generateAccessTokenModel, cancellationToken);
                
                accessToken = accessToken?.Trim('"');
                
                CookieHelper.SetShare(HttpContext, GoblinCookieKeys.AccessToken, accessToken);
                
                return View("LoggedIn");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
                return View("Login", model);
            }
        }

        [Route(Endpoints.Logout)]
        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
    }
}