using Microsoft.AspNetCore.Mvc;
using WebApp2BySupriya.Models;
using System.Text.Json;

namespace WebApp2BySupriya.Controllers
{
    public class StudentController : Controller
    {
        // GET: /Student/MyRazorPage
        public IActionResult MyRazorPage()
        {
            ViewData["Name"] = "Supriya Devkota";
            ViewData["RollNo"] = 25;
            return View();
        }

        // GET: /Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                // Store student data in TempData to pass across redirect safely
                TempData["SubmittedStudent"] = JsonSerializer.Serialize(student);
                TempData["SuccessMessage"] = "Student record created successfully!";
                return RedirectToAction(nameof(Details));
            }

            // Model validation failed, return form view with error messages
            return View(student);
        }

        // GET: /Student/Details
        public IActionResult Details()
        {
            if (TempData["SubmittedStudent"] is string studentJson)
            {
                var student = JsonSerializer.Deserialize<Student>(studentJson);
                // Keep TempData available for page reloads if needed
                TempData.Keep("SubmittedStudent");
                return View(student);
            }

            // If accessed directly without form submission, return demo/default record or redirect
            var defaultStudent = new Student
            {
                StdID = 101,
                Name = "Supriya Devkota",
                Address = "Kathmandu, Nepal",
                Faculty = "BSc CSIT",
                Email = "supriya@example.com"
            };

            return View(defaultStudent);
        }
    }
}
