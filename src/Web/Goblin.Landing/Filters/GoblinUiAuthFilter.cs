using Elect.DI.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Goblin.Landing.Filters
{
    [ScopedDependency]
    public class GoblinUiAuthFilter: IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAuthenticate = GoblinUiAuthHelper.IsAuthenticated(context);

            if (!isAuthenticate)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null, false);

                return;
            }
            
            var isAuthorization = GoblinUiAuthHelper.IsAuthorized(context);
            
            if (!isAuthorization)
            {
                context.Result = new RedirectToActionResult("Error", "Home", new {code = StatusCodes.Status403Forbidden}, false);
            }
        }
    }
}