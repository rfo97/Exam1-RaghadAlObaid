using Exam1.Data;
using Exam1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exam1.Controllers
{
    public class InstructorsController : Controller
    {
        private AppDbContext db;
        public InstructorsController(AppDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View(db.Instructors);
        }

        #region Read
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var instructor = db.Instructors.Find(id);
            if (instructor != null)
            {
                return View(instructor);

            }
            return RedirectToAction(nameof(Index));
        }
        #endregion Read

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Instructor instructor)
        {
            db.Instructors.Add(instructor);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        #endregion Create

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var instructor = db.Instructors.Find(id);
            if (instructor != null)
            {
                return View(instructor);

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(Instructor instructor)
        {
            db.Instructors.Update(instructor);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        #endregion Edit

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var instructor = db.Instructors.Find(id);
            if (instructor != null)
            {
                return View(instructor);

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ConfirmDelete(Instructor instructor)
        {
            var DeleteInstructor = db.Instructors.Find(instructor.Id);
            db.Instructors.Remove(DeleteInstructor);
            db.SaveChanges();
            return DeleteInstructor != null ? RedirectToAction(nameof(Index)) : RedirectToAction(nameof(Index));
        }
        #endregion Delete



    }
}
