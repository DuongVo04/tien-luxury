using MinhTienHairSalon.Models;
using MinhTienHairSalon.ViewModels;

namespace MinhTienHairSalon.Areas.Admin.ViewModels
{
    public class InvoiceUpdateVIewModel
    {
        public Invoice Invoice { get; set; }
        public List<CartItemViewModel>? Items { get; set; }
    }
}