using Microsoft.AspNetCore.Mvc;
using WebApp3BySupriya.Services;

namespace WebApp3BySupriya.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITransientService _transient1;
        private readonly ITransientService _transient2;
        private readonly IScopedService _scoped1;
        private readonly IScopedService _scoped2;
        private readonly ISingletonService _singleton1;
        private readonly ISingletonService _singleton2;
        private readonly DILifetimeDemoService _demoService;

        public HomeController(
            ITransientService transient1,
            ITransientService transient2,
            IScopedService scoped1,
            IScopedService scoped2,
            ISingletonService singleton1,
            ISingletonService singleton2,
            DILifetimeDemoService demoService)
        {
            _transient1 = transient1;
            _transient2 = transient2;
            _scoped1 = scoped1;
            _scoped2 = scoped2;
            _singleton1 = singleton1;
            _singleton2 = singleton2;
            _demoService = demoService;
        }

        public IActionResult Index()
        {
            ViewBag.Transient1 = _transient1.OperationId;
            ViewBag.Transient2 = _transient2.OperationId;
            ViewBag.TransientServiceInjected = _demoService.TransientService.OperationId;

            ViewBag.Scoped1 = _scoped1.OperationId;
            ViewBag.Scoped2 = _scoped2.OperationId;
            ViewBag.ScopedServiceInjected = _demoService.ScopedService.OperationId;

            ViewBag.Singleton1 = _singleton1.OperationId;
            ViewBag.Singleton2 = _singleton2.OperationId;
            ViewBag.SingletonServiceInjected = _demoService.SingletonService.OperationId;

            return View();
        }
    }
}
