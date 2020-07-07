using Elect.DI.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Goblin.Landing.Filters
{
    [ScopedDependency]
    public class GoblinUiAuthFilter: IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAuthenticate = GoblinUiAuthHelper.IsAuthenticate(context);

            if (!isAuthenticate)
            {
                context.Result = new RedirectToActionResult("Index", "Home", null, false);
            }
        }
    }
}