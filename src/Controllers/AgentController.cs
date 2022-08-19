using Microsoft.AspNetCore.Mvc;

namespace SimpleSignalRChatApp.Controllers
{
    public class AgentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
