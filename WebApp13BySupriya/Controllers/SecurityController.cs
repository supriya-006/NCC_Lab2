using Microsoft.AspNetCore.Mvc;

namespace WebApp13BySupriya.Controllers
{
    public class SecurityController : Controller
    {
        public IActionResult Xss()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Xss(string message)
        {
            ViewBag.Message = message;
            return View();
        }
    }
}
