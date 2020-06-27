using Elect.DI.Attributes;
using Goblin.Core.Constants;
using Goblin.Core.Web.Utils;
using Goblin.Landing.Core;
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
            var isAuthenticate = GoblinUiAuthHelper.IsAuthenticate(context);

            if (!isAuthenticate)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
            
            var userId = context.HttpContext.GetKey<long>(GoblinHeaderKeys.UserId);

            LoggedInUser.Current = new LoggedInUser(userId);
        }
    }
}