using Exam1.Data;
using Exam1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exam1.Controllers
{
    public class StudentsController : Controller
    {
        private AppDbContext db;
        public StudentsController(AppDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View(db.Students);
        }

        #region Read
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var student = db.Students.Find(id);
            if (student != null)
            {
                return View(student);

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
        public IActionResult Create(Student student)
        {
            db.Students.Add(student);
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
            var student = db.Students.Find(id);
            if (student != null)
            {
                return View(student);

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            db.Students.Update(student);
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
            var student = db.Students.Find(id);
            if (student != null)
            {
                return View(student);

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ConfirmDelete(Student student)
        {
            var DeleteStudent = db.Students.Find(student.Id);
            db.Students.Remove(DeleteStudent);
            db.SaveChanges();
            return DeleteStudent != null ? RedirectToAction(nameof(Index)) : RedirectToAction(nameof(Index));
        }
        #endregion Delete




    }
}