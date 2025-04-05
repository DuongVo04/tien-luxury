using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MinhTienHairSalon.Areas.Admin.Services;
using MinhTienHairSalon.Areas.Admin.ViewModels;
using System.Threading.Tasks;
using MinhTienHairSalon.Areas.Filter;

namespace MinhTienHairSalon.Areas.Admin.Controllers
{
    [Area("Admin")]
    [DesktopOnly]
    public class LoginController(IAdminAccountService adminAccountService) : Controller
    {
        private readonly IAdminAccountService _adminAccountService = adminAccountService;

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(AdminAccountViewModel adminAccountViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Thông tin không hợp lệ!";
                return View();
            }

            if (_adminAccountService.CheckAccount(adminAccountViewModel.AdminAccount))
            {
                // Lưu ID vào Session
                HttpContext.Session.SetString("AdminID", _adminAccountService.FindIdByUserName(adminAccountViewModel.AdminAccount.Username).ToString());
                HttpContext.Session.SetString("AdminLoggedIn", "true"); // Lưu trạng thái đăng nhập vào Session
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Đăng nhập thất bại. Vui lòng kiểm tra lại!";
            return View();
        }


    }
}
