using Exam1.Data;
using Exam1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Exam1.Controllers
{
    public class CoursesController : Controller
    {
        private AppDbContext db;
        public CoursesController(AppDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var data = db.Courses.Include(i => i.Instructor);
            return View(data);
        }

        #region Read
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var course = db.Courses.Find(id);
            if (course != null)
            {
                return View(course);

            }
            return RedirectToAction(nameof(Index));
        }
        #endregion Read

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AllInstructors = new SelectList(db.Instructors, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            
            db.Courses.Add(course);
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
            ViewBag.AllInstructors = new SelectList(db.Instructors, "Id", "FullName");
            var course = db.Courses.Find(id);
            if (course != null)
            {
                return View(course);

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            db.Courses.Update(course);
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
            var course = db.Courses.Find(id);
            if (course != null)
            {
                return View(course);

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ConfirmDelete(Course course)
        {
            var DeleteCourse = db.Courses.Find(course.Id);
            db.Courses.Remove(DeleteCourse);
            db.SaveChanges();
            return DeleteCourse != null ? RedirectToAction(nameof(Index)) : RedirectToAction(nameof(Index));
        }
        #endregion Delete

    }
}
