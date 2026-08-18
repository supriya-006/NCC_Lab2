using Microsoft.AspNetCore.Mvc;

namespace WebApp9BySupriya.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string? message)
        {
            // Read query string
            ViewData["Query"] = Request.Query["q"].ToString();

            // Read cookie
            ViewData["Cookie"] = Request.Cookies["MyCookie"] ?? "<none>";

            // Hidden field sample value
            ViewData["HiddenValue"] = message ?? "default-hidden";

            return View();
        }

        [HttpPost]
        public IActionResult SetCookie(string value)
        {
            Response.Cookies.Append("MyCookie", value);
            return RedirectToAction("Index");
        }
    }
}
