using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp10BySupriya.Controllers
{
    public class HomeController : Controller
    {
        // Anyone can access this page
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        // Only logged-in users can access this page
        [Authorize]
        public IActionResult Secure()
        {
            return View();
        }
    }
}