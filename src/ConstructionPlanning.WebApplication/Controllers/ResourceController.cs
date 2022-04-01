using Microsoft.AspNetCore.Mvc;

namespace ConstructionPlanning.WebApplication.Controllers
{
    public class ResourceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
