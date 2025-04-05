using Microsoft.AspNetCore.Mvc;

namespace MinhTienHairSalon.Areas.Admin.Controllers
{
    public class DeviceNotSupported : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}