using Microsoft.AspNetCore.Mvc;
using TienLuxury.Helpers;
using TienLuxury.Models;
using TienLuxury.Services;
using TienLuxury.ViewModels;
using MongoDB.Bson;

namespace TienLuxury.Controllers
{
    public class ShoppingCartController(IInvoiceService invoiceService, IInvoiceDetailsService invoiceDetailsService, IProductService productService) : Controller
    {
        private readonly IInvoiceService _invoiceService = invoiceService;
        private readonly IInvoiceDetailsService _invoiceDetailService = invoiceDetailsService;
        private readonly IProductService _productService = productService;

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>("Cart") ?? [];

            OrderViewModel model = new()
            {
                Items = cart
            };

            return View(model);
        }

        public IActionResult Decrease(string productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>("Cart");

            if (cart != null)
            {
                var item = cart.FirstOrDefault(i => i.ProductID == productId.ToString());
                if (item != null)
                {
                    item.Quantity--;

                    if (item.Quantity <= 0)
                    {
                        cart.Remove(item);
                    }

                    HttpContext.Session.SetObjectAsJson("Cart", cart);
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Increase(ObjectId productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>("Cart");

            if (cart != null)
            {
                var item = cart.FirstOrDefault(i => i.ProductID == productId.ToString());
                if (item != null)
                {
                    item.Quantity++;
                    HttpContext.Session.SetObjectAsJson("Cart", cart);
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Remove(ObjectId productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>("Cart");

            if (cart != null)
            {
                var item = cart.FirstOrDefault(i => i.ProductID == productId.ToString());
                if (item != null)
                {
                    cart.Remove(item);
                    HttpContext.Session.SetObjectAsJson("Cart", cart);
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CreateInvoice(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cart = HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>("Cart");

                if (cart != null)
                {

                    Invoice newInvoice = new()
                    {
                        CustomerName = model.CustomerName,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        Address = model.Address,
                        CreatedDate = DateTime.Now,
                        PaymentMethod = model.PaymentMethod
                    };

                    ObjectId invoiceId = await _invoiceService.CreateInvoice(newInvoice);
                    decimal total = 0;

                    SuccessfulViewModel successfulViewModel = new()
                    {
                        Items = []
                    };

                    foreach (var c in cart)
                    {
                        successfulViewModel.Items.Add(c);
                        InvoiceDetail detail = new()
                        {
                            InvoiceId = invoiceId,
                            ProductId = ObjectId.Parse(c.ProductID),
                            Quantity = c.Quantity
                        };

                        total += c.ProductPrice * c.Quantity;

                        await _invoiceDetailService.CreateInvoiceDetail(detail);

                        newInvoice.InvoiceDetails.Add(detail);
                        await _productService.MinusQuantityInStock(ObjectId.Parse(c.ProductID), c.Quantity);

                    }

                    await _invoiceService.UpdateTotal(newInvoice, total);

                    successfulViewModel.Invoice = newInvoice;
                    HttpContext.Session.Remove("Cart");

                    return View("Successful", successfulViewModel);

                }
            }

            ViewBag.ErrorMessage = "Lỗi khi truy xuất thông tin";
            return RedirectToAction("Index");

        }

        public IActionResult Successful()
        {
            return View();
        }
    }
}