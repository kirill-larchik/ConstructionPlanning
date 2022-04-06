using Microsoft.AspNetCore.Mvc;

namespace ConstructionPlanning.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
