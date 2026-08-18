using Microsoft.AspNetCore.Mvc;
using WebApp5BySupriya.Data;
using WebApp5BySupriya.Models;

namespace WebApp5BySupriya.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentRepository _repo;

        public StudentController(StudentRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var students = _repo.GetAll();
            return View(students);
        }

        public IActionResult Details(int id)
        {
            var s = _repo.GetById(id);
            if (s == null) return NotFound();
            return View(s);
        }

        public IActionResult Create()
        {
            return View(new Student());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (!ModelState.IsValid) return View(student);
            _repo.Create(student);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var s = _repo.GetById(id);
            if (s == null) return NotFound();
            return View(s);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            if (!ModelState.IsValid) return View(student);
            if (!_repo.Update(student)) return NotFound();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var s = _repo.GetById(id);
            if (s == null) return NotFound();
            return View(s);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
