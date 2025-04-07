using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;
using MinhTienHairSalon.Areas.Admin.ViewModels;
using MinhTienHairSalon.Areas.Admin.Services;
using MinhTienHairSalon.Areas.Filter;
using MinhTienHairSalon.Services;
using System.Threading.Tasks;

namespace MinhTienHairSalon.Areas.Admin.Controllers
{
    [AdminAuth]
    [Area("Admin")]
    [DesktopOnly]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class HomeController(IAdminAccountService adminAccountService, IMessageService messageService,
        IServiceService serviceService, IProductService productService,
        IReservationService reservationService, IInvoiceService invoiceService) : Controller
    {
        private readonly IAdminAccountService _adminAccountService = adminAccountService;
        private readonly IServiceService _serviceService = serviceService;
        private readonly IProductService _productService = productService;
        private readonly IReservationService _reservationService = reservationService;
        private readonly IInvoiceService _invoiceService = invoiceService;
        private readonly IMessageService _messageService = messageService;

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("AdminLoggedIn") == "true")
            {

                HomeViewModel model = new()
                {
                    ReservationsToDay = _reservationService.GetAllReservation().Result.ToList().Count(x => x.ReservationDate == DateTime.Now.Date),
                    OrdersToday = _invoiceService.GetAllInvoices().Result.ToList().Count(x => x.Status == "Đang xử lý"),
                    NumberOfServices = _serviceService.GetAllServices().Result.ToList().Count,
                    NumberOfProducts = _productService.GetAllProduct().Result.ToList().Count,
                };

                model.MearestAppointment = _reservationService.GetAllReservation().Result.ToList()
                    .Where(x => x.ReservationDate >= DateTime.Now)
                    .OrderBy(x => x.ReservationDate).FirstOrDefault(x => x.ReservationStatus == "Chưa ghé")?.ReservationDate ?? DateTime.Now.Date;

                model.LastOrder = (DateTime.Now - _invoiceService.GetAllInvoices().Result.ToList()
                    .OrderByDescending(x => x.CreatedDate)?
                    .FirstOrDefault(x => x.Status == "Đang xử lý")?.CreatedDate.ToLocalTime())?.Hours ?? 0;

                model.LastMessage = (DateTime.Now - _messageService.GetAllMessage().Result.ToList()?
                    .FirstOrDefault()?.CreatedAt.ToLocalTime())?.Hours ?? 0;

                return View(model);
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
            => PartialView();


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
            => RedirectToAction("Index", "ServicesManagement");


        public IActionResult ProductsManagement()
            => RedirectToAction("Index", "ProductsManagement");


        public IActionResult OrdersManagement()
            => RedirectToAction("Index", "OrdersManagement");


        public IActionResult ReservationsManagement()
            => RedirectToAction("Index", "ReservationsManagement");


        public IActionResult EmployeesManagement()
            => RedirectToAction("Index", "EmployeesManagement");


        public IActionResult MessagesManagement()
            => RedirectToAction("Index", "MessagesManagement");

    }
}