using Microsoft.AspNetCore.Mvc;

namespace MinhTienHairSalon.Areas.Admin.Controllers
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