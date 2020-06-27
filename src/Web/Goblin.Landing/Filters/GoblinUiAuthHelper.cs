using Microsoft.AspNetCore.Mvc.Filters;

namespace Goblin.Landing.Filters
{
    public static class GoblinUiAuthHelper
    {
        public static bool IsAuthenticate(AuthorizationFilterContext context)
        {
            return true;
        }
    }
}