using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SimpleSignalRChatApp.Controllers
{
    [Authorize]
    public class AgentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
