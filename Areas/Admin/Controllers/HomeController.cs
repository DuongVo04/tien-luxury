using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;
using MinhTienHairSalon.Areas.Admin.ViewModels;
using MinhTienHairSalon.Areas.Admin.Services;
using MinhTienHairSalon.Areas.Filter;

namespace MinhTienHairSalon.Areas.Admin.Controllers
{
    [AdminAuth]
    [Area("Admin")]
    [DesktopOnly]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class HomeController(IAdminAccountService adminAccountService) : Controller
    {
        private readonly IAdminAccountService _adminAccountService = adminAccountService;

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("AdminLoggedIn") == "true")
            {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult SignUp(AdminAccountViewModel adminAccountViewModel)
        {
            _adminAccountService.CreateAccount(adminAccountViewModel.AdminAccount);

            return RedirectToAction("Index", "Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AdminID"); // Xóa ID admin trong sesstion
            HttpContext.Session.Remove("AdminLoggedIn"); // Xóa Session đăng nhập
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            try
            {
                if (_adminAccountService.ChangePassword(ObjectId.Parse(HttpContext.Session.GetString("AdminID")), newPassword, oldPassword))
                {
                    return Json(new { success = true, message = "Thay đổi mật khẩu thành công." });
                }
                return Json(new { success = false, message = "Mật khẩu cũ không đúng." });
            }
            catch
            {
                throw;
            }

        }

        public IActionResult ServicesManagement()
        {
            return RedirectToAction("Index", "ServicesManagement");
        }

        public IActionResult ProductsManagement()
        {
            return RedirectToAction("Index", "ProductsManagement");
        }

        public IActionResult OrdersManagement()
        {
            return RedirectToAction("Index", "OrdersManagement");
        }

        public IActionResult ReservationsManagement()
        {
            return RedirectToAction("Index", "ReservationsManagement");
        }

        public IActionResult EmployeesManagement()
        {
            return RedirectToAction("Index", "EmployeesManagement");
        }
    }
}