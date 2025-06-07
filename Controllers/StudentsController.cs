using Exam1.Data;
using Exam1.Models;
using Exam1.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var student = db.Students
                            .Include(s => s.StudentCourses)
                            .ThenInclude(sc => sc.Course)
                            .FirstOrDefault(s => s.Id == id);
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

        #region Enroll Student
        public IActionResult EnrollInCourse(int? id)
        {
            var student = db.Students.Find(id);
            if (student != null) 
            {
                List<Course> courses = db.Courses.ToList();
                var enrolledCourseIds =  db.StudentCourses
                        .Where(s => s.StudentId == id)
                        .Select(s => s.CourseId)
                        .ToList();

                var model = new EnrollViewModel
                {
                    Student = student,
                    Courses = courses,
                    EnrolledCoursesIds = enrolledCourseIds
                };
                return View(model);
            }
            return View("Index","Courses");
        }

        [HttpPost]
        public IActionResult EnrollInCourse(int sid,List<int> CoursesIds)
        {
            CoursesIds = CoursesIds == null ? new List<int>() : CoursesIds;
            foreach (var item in CoursesIds)
            {
                db.StudentCourses.Add(new StudentCourse
                {
                    StudentId = sid,
                    CourseId = item
                });
            }
            db.SaveChanges();
            return RedirectToAction(nameof(Details), new { id = sid });
        }


        #endregion


    }
}