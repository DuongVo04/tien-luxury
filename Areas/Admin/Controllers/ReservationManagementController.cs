using Microsoft.AspNetCore.Mvc;
using MinhTienHairSalon.Services;
using MinhTienHairSalon.Areas.Admin.ViewModels;
using MinhTienHairSalon.Models;
using MongoDB.Bson;
using MinhTienHairSalon.ViewModels;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;

namespace MinhTienHairSalon.Areas.Admin.Controllers
{
    [AdminAuth]
    [Area("Admin")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class ReservationsManagementController(IReservationService reservationService, IServiceService serviceService, IReservationDetailService reservationDetailService) : Controller
    {   
        private readonly IReservationService _reservationService = reservationService;
        private readonly IServiceService _serviceService = serviceService;
        private readonly IReservationDetailService _reservationDetailService = reservationDetailService;

        public async Task<IActionResult> Index()
        {
            ReservationListViewModel model = new()
            {
                Reservations = await _reservationService.GetAllReservation(),
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateReservationStatus(ObjectId id)
        {
            Reservation? reservation = await _reservationService.GetBookingByID(id);
            reservation.CreatedDate = new DateTime(
                reservation.CreatedDate.Year,
                reservation.CreatedDate.Month,
                reservation.CreatedDate.Day,
                reservation.CreatedDate.Hour,
                reservation.CreatedDate.Minute,
                0  // Đặt giây = 0
            );


            if (reservation != null)
            {
                List<ReservationDetail> details = (await _reservationDetailService.GetAllDetailsByReservationID(id)).ToList();
                List<Service> servicesBooked = new();

                IEnumerable<Service> allServices = await _serviceService.GetAllServices();

                foreach(ReservationDetail d in details)
                {
                    servicesBooked.Add(allServices.FirstOrDefault(s => s.ID == d.ServiceID));
                }

                ReservationAddEditViewModel model = new()
                {
                    Reservation = await _reservationService.GetBookingByID(id),
                    Services = servicesBooked
                };

                return  View(model);
            }
            return Json(new { success = false, message = "Reservation not found" });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReservationStatus(ReservationAddEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _reservationService.UpdateReservationStatus(model.Reservation);

                return Redirect("Index");
            }
            return Json(new { success = false, message = "Update failed" });
        }
    }
}
