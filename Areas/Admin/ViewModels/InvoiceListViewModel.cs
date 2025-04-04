using MinhTienHairSalon.Models;

namespace MinhTienHairSalon.Areas.Admin.ViewModels
{
    public class InvoiceListViewModel
    {
        public IEnumerable<Invoice> Invoices { get; set; }
    }
}