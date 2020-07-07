using System;
using Goblin.Core.Constants;
using Goblin.Core.Utils;
using Goblin.Identity.Share;
using Goblin.Identity.Share.Models.UserModels;
using Goblin.Landing.Core;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Goblin.Landing.Filters
{
    public static class GoblinUiAuthHelper
    {
        public static bool IsAuthenticate(AuthorizationFilterContext context)
        {
            var accessToken = CookieHelper.GetShare<string>(context.HttpContext, GoblinCookieKeys.AccessToken);

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return false;
            }

            try
            {
                var userModelTask = GoblinIdentityHelper.GetProfileByAccessTokenAsync(accessToken);

                userModelTask.Wait();

                var userModel = userModelTask.Result;

                LoggedInUser<GoblinIdentityUserModel>.Current = new LoggedInUser<GoblinIdentityUserModel>(userModel);
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
                return false;
            }
        }
    }
}