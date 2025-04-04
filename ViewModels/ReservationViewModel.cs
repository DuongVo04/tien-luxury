using MinhTienHairSalon.Models;

namespace MinhTienHairSalon.ViewModels
{
    public class ReservationViewModel
    {
        public Reservation Reservation { get; set; }
        public List<Service> Services { get; set; }
    }
}