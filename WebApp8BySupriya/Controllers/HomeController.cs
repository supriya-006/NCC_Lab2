using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebApp8BySupriya.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache _cache;
        public HomeController(IMemoryCache cache) { _cache = cache; }

        public IActionResult Index()
        {
            // Session
            HttpContext.Session.SetString("SessionName", "SessionValue");

            // HttpContext.Items
            HttpContext.Items["ItemKey"] = "ItemValue";

            // TempData
            TempData["TempKey"] = "TempValue";

            // MemoryCache
            _cache.Set("CacheKey", "CacheValue");

            ViewData["Session"] = HttpContext.Session.GetString("SessionName");
            ViewData["Item"] = HttpContext.Items["ItemKey"]?.ToString();
            ViewData["Temp"] = TempData["TempKey"]?.ToString();
            ViewData["Cache"] = _cache.Get<string>("CacheKey");

            return View();
        }
    }
}
