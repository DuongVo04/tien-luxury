using Microsoft.AspNetCore.Mvc;

namespace TienLuxury.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DeviceNotSupportedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}