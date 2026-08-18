using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp7BySupriya.Data;
using WebApp7BySupriya.Data.Entities;

namespace WebApp7BySupriya.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _db;
        public StudentsController(SchoolContext db) { _db = db; }

        public IActionResult Index()
        {
            var list = _db.Students.OrderBy(s => s.Id).ToList();
            return View(list);
        }

        public IActionResult Details(int id)
        {
            var s = _db.Students.Find(id);
            if (s == null) return NotFound();
            return View(s);
        }

        public IActionResult Create() => View(new Student());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (!ModelState.IsValid) return View(student);
            _db.Students.Add(student); _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id) { var s = _db.Students.Find(id); if (s==null) return NotFound(); return View(s); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student) { if (!ModelState.IsValid) return View(student); _db.Students.Update(student); _db.SaveChanges(); return RedirectToAction(nameof(Index)); }

        public IActionResult Delete(int id) { var s = _db.Students.Find(id); if (s==null) return NotFound(); return View(s); }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id) { var s = _db.Students.Find(id); if (s==null) return NotFound(); _db.Students.Remove(s); _db.SaveChanges(); return RedirectToAction(nameof(Index)); }
    }
}
