using Elect.DI.Attributes;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Goblin.Landing.Filters
{
    [ScopedDependency]
    public class GoblinUiLoggedInUserFilter: IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            GoblinUiAuthHelper.BindLoggedInUser(context.HttpContext);
        }
    }
}