namespace MinhTienHairSalon.Areas.Admin.ViewModels
{
    public class HomeViewModel
    {
        public int ReservationsToDay { get; set; } = 0;
        public int OrdersToday { get; set; } = 0;
        public int NumberOfServices { get; set; } = 0;
        public int NumberOfProducts { get; set; } = 0;

        public DateTime MearestAppointment { get; set; } = DateTime.Now;
        public int LastOrder { get; set; } = 0;
        public int LastMessage { get; set; } = 0;

    }
}