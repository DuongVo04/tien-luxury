using Microsoft.AspNetCore.Mvc;
using MinhTienHairSalon.Areas.Admin.ViewModels;
using MinhTienHairSalon.Models;
using MinhTienHairSalon.Services;
using MinhTienHairSalon.ViewModels;
using MongoDB.Bson;

namespace MinhTienHairSalon.Areas.Admin.Controllers
{
    [AdminAuth]
    [Area("Admin")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class OrdersManagementController(IInvoiceService invoiceService, IInvoiceDetailsService invoiceDetailsService, IProductService productService) : Controller
    {
        private readonly IInvoiceService _invoiceService = invoiceService;
        private readonly IInvoiceDetailsService _invoiceDetailService = invoiceDetailsService;
        private readonly IProductService _productService = productService;
        public async Task<IActionResult> Index()
        {
            InvoiceListViewModel model = new()
            {
                Invoices = await _invoiceService.GetAll()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateInvoiceStatus(ObjectId invoiceID)
        {
            Invoice invoice = await _invoiceService.GetInvoiceById(invoiceID);
            if (invoice == null)
            {
                return NotFound("Invoice not found.");
            }

            var invoiceDetailsTask = _invoiceDetailService.GetDetailsByInvoiceId(invoiceID);
            var allProductsTask = _productService.GetAllProduct();

            await Task.WhenAll(invoiceDetailsTask, allProductsTask);

            invoice.InvoiceDetails = invoiceDetailsTask.Result;
            var allProducts = allProductsTask.Result;

            var productDetailsMap = allProducts.ToDictionary(p => p.ID, p => p);

            var cartItems = invoice.InvoiceDetails
                .Where(d => productDetailsMap.ContainsKey(d.ProductId))
                .Select(d =>
                {
                    var product = productDetailsMap[d.ProductId];
                    return new CartItemViewModel
                    {
                        ProductID = product.ID.ToString(),
                        ProductName = product.ProductName,
                        ProductPrice = (int)product.Price,
                        ImagePath = product.ImagePath,
                        Quantity = d.Quantity
                    };
                }).ToList();

            var model = new InvoiceUpdateVIewModel
            {
                Invoice = invoice,
                Items = cartItems
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateInvoiceStatus(InvoiceUpdateVIewModel model)
        {
            var invoice = await _invoiceService.GetInvoiceById(model.Invoice.ID);
            if (invoice == null)
            {
                return NotFound("Invoice not found.");
            }

            invoice.Status = model.Invoice.Status;
            await _invoiceService.UpdateStatusInvoice(invoice);

            TempData["SuccessMessage"] = "Invoice status updated successfully.";
            return RedirectToAction("Index");

        }
    }
}
