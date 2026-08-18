using Microsoft.AspNetCore.Mvc;
using WebApp4BySupriya.Models;
using WebApp4BySupriya.Services;

namespace WebApp4BySupriya.Controllers
{
    public class JsonController : Controller
    {
        private readonly JsonService _jsonService;
        private readonly IWebHostEnvironment _env;

        public JsonController(JsonService jsonService, IWebHostEnvironment env)
        {
            _jsonService = jsonService;
            _env = env;
        }

        private string GetJsonFilePath()
        {
            return Path.Combine(_env.WebRootPath, "data", "books.json");
        }

        // GET: /Json/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Json/InMemory
        public IActionResult InMemory()
        {
            var (rawJson, books, serializedJson) = _jsonService.DemonstrateInMemoryJsonParsing();
            ViewBag.RawJson = rawJson;
            ViewBag.SerializedJson = serializedJson;
            return View(books);
        }

        // GET: /Json/FromFile
        public async Task<IActionResult> FromFile()
        {
            string filePath = GetJsonFilePath();
            List<Book> books = await _jsonService.ReadBooksFromFileAsync(filePath);
            
            if (System.IO.File.Exists(filePath))
            {
                ViewBag.RawFileContent = await System.IO.File.ReadAllTextAsync(filePath);
            }

            return View(books);
        }

        // POST: /Json/FromFile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FromFile(Book newBook)
        {
            if (ModelState.IsValid)
            {
                string filePath = GetJsonFilePath();
                await _jsonService.AddBookAndSaveToFileAsync(filePath, newBook);
                TempData["SuccessMessage"] = $"Book '{newBook.Title}' saved to books.json successfully!";
                return RedirectToAction(nameof(FromFile));
            }

            string path = GetJsonFilePath();
            List<Book> books = await _jsonService.ReadBooksFromFileAsync(path);
            if (System.IO.File.Exists(path))
            {
                ViewBag.RawFileContent = await System.IO.File.ReadAllTextAsync(path);
            }
            return View(books);
        }
    }
}
