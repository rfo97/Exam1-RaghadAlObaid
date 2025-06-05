using Microsoft.AspNetCore.Mvc;

namespace Exam1.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
