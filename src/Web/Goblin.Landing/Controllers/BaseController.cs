using Goblin.Landing.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Goblin.Landing.Controllers
{
    [ServiceFilter(typeof(GoblinUiAuthFilter))]
    public class BaseController : Controller
    {
    }
}