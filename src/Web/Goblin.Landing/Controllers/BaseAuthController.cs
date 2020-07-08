using Goblin.Landing.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Goblin.Landing.Controllers
{
    [ServiceFilter(typeof(GoblinUiExceptionFilterAttribute))]
    [ServiceFilter(typeof(GoblinUiLoggedInUserFilter))]
    [ServiceFilter(typeof(GoblinUiAuthFilter))]
    public class BaseAuthController : BaseController
    {
    }
}