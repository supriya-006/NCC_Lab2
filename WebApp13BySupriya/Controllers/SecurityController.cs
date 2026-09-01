using Microsoft.AspNetCore.Mvc;

namespace WebApp13BySupriya.Controllers
{
    public class SecurityController : Controller
    {
        // XSS code
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

        // ADD THESE
        public IActionResult Csrf()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangeName(string name)
        {
            ViewBag.Message = "Name changed to: " + name;
            return View("Csrf");
        }
    }
}