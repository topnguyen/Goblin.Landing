using Elect.Web.IUrlHelperUtils;
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
        public IActionResult SubmitLogin([FromForm] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }
            
            // TODO Login
            
            if (string.IsNullOrWhiteSpace(model.Continue))
            {
                model.Continue = Url.AbsoluteAction("Index", "Home");
            }

            ViewBag.ContinueUrl = model.Continue;

            return View("LoggedIn");
        }

        [Route(Endpoints.Logout)]
        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
    }
}