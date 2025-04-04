using MinhTienHairSalon.Models;

namespace MinhTienHairSalon.ViewModels
{
    public class SuccessfulViewModel
    {
        public Invoice Invoice { get; set; }
        public List<CartItemViewModel> Items { get; set; }
    }
}