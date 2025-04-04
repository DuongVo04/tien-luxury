using MinhTienHairSalon.Models;

namespace MinhTienHairSalon.ViewModels
{
    public class ServiceByTypeListViewModel
    {
        public List<Service> Hair { get; set; }

        public List<Service> SkinCare { get; set; }

        public List<Service> Others { get; set; }
    }
}