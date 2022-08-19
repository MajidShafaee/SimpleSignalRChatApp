using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SimpleSignalRChatApp.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index(string ReturnUrl)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromQuery] string returnUrl = null)
        {
            

            var username = Request.Form["username"];
            var password = Request.Form["password"];
           
            if (password != "password") return LocalRedirect("/login");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Support"),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var properties = new AuthenticationProperties
            {
                RedirectUri = returnUrl ?? Url.Content("~/agent")
            };
            
          await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), properties);
            return LocalRedirectPermanent("/agent");
            
        }
    }
}
