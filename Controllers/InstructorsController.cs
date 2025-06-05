using Microsoft.AspNetCore.Mvc;

namespace Exam1.Controllers
{
    public class InstructorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
