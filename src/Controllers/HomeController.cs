using Microsoft.AspNetCore.Mvc;
using SimpleSignalRChatApp.Models;
using SimpleSignalRChatApp.Services;
using System.Diagnostics;

namespace SimpleSignalRChatApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessorService _httpContextAccessorService;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessorService httpContextAccessorService)
        {
            _httpContextAccessorService = httpContextAccessorService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _httpContextAccessorService.SetChatCookie(HttpContext);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}