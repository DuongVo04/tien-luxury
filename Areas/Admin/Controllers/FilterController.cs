using Microsoft.AspNetCore.Mvc;

namespace MinhTienHairSalon.Areas.Admin.Controllers
{
    public class FilterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}