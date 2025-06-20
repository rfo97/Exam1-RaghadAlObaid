﻿using Exam1.Data;
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
       
        public IActionResult Index(string? searchStr, int? InstructorId)
        {
            ViewBag.AllInstructors = new SelectList(db.Instructors, "Id", "FullName");
            ViewBag.SelectedInstructorId = InstructorId?.ToString() ?? "";
            var data = db.Courses.Include(i => i.Instructor).AsQueryable();

            if (!string.IsNullOrEmpty(searchStr))
            {
                var filteredByTitle = data.Where(p => p.Title.Contains(searchStr));
                var filteredByInstructor = data.Where(c => c.InstructorId == InstructorId);
                var filteredByDescription = data.Where(p => p.Description.Contains(searchStr));

                if (filteredByTitle.Any())
                {
                    data = filteredByTitle;
                    ViewData["CurrentTitleFilter"] = searchStr;
                }
                else if (filteredByInstructor.Any())
                {
                    data = filteredByInstructor;
                    ViewData["CurrentInstructorFilter"] = searchStr;
                }
                else
                {
                    data = filteredByDescription;
                    ViewData["CurrentDescriptionFilter"] = searchStr;
                }
            }

            else
            {
                var coursesQuery = db.Courses.AsQueryable();
                if (InstructorId.HasValue)
                {
                    coursesQuery = coursesQuery.Where(c => c.InstructorId == InstructorId.Value);
                    data = coursesQuery;
                }

            }
                return View(data.ToList());
        }

        #region Read
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var course = db.Courses
                        .Include(i => i.Instructor)
                        .Include(s => s.StudentCourses)
                        .ThenInclude(sc => sc.Student)
                        .FirstOrDefault(s => s.Id == id);

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
