using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TienLuxury.Helpers;
using TienLuxury.Models;
using TienLuxury.Services;
using TienLuxury.ViewModels;
using MongoDB.Bson;

namespace TienLuxury.Controllers
{
    public class ProductController(IProductService productService) : Controller
    {
        private readonly IProductService _productService = productService;

        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> allProducts = await _productService.GetAllProduct();

            ProductByTypeListViewModel model = new()
            {
                Hair = new List<Product>(),
                SkinCare = new List<Product>(),
                Others = new List<Product>(),
                QuantityInCart = (HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>("Cart")?.Count ?? 0)
            };

            foreach (var p in allProducts)
            {
                if (p.ProductType == "Mỹ phẩm tóc")
                {
                    model.Hair.Add(p);
                }
                else
                {
                    if (p.ProductType == "Skin care")
                    {
                        model.SkinCare.Add(p);
                    }
                    else
                    {
                        model.Others.Add(p);
                    }
                }

            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetail(ObjectId id)
        {
            ProductDetailViewModel model = new()
            {
                Product = await _productService.GetProductById(id) ?? throw new NullReferenceException("Product not found"),
                Products = (await _productService.GetAllProduct()).ToList()
            };

            // Console.Write(id);
            return View(model);
        }

        public async Task<IActionResult> AddToCart(ProductDetailViewModel model)
        {

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>("Cart") ?? [];
            var CartItem = cart.FirstOrDefault(c => c.ProductID == model.CartItemViewModel.ProductID);

            if (CartItem == null)
            {
                cart.Add(
                    new CartItemViewModel()
                    {
                        ProductID = model.CartItemViewModel.ProductID,
                        ProductName = model.CartItemViewModel.ProductName,
                        ProductPrice = model.CartItemViewModel.ProductPrice,
                        ImagePath = model.CartItemViewModel.ImagePath,
                        Quantity = model.CartItemViewModel.Quantity,
                    }
                );
            }
            else
            {
                CartItem.Quantity++;
            }

            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult BuyNow(ProductDetailViewModel model)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>("Cart") ?? [];
            var CartItem = cart.FirstOrDefault(c => c.ProductID == model.CartItemViewModel.ProductID);

            if (CartItem == null)
            {
                cart.Add(
                    new CartItemViewModel()
                    {
                        ProductID = model.CartItemViewModel.ProductID,
                        ProductName = model.CartItemViewModel.ProductName,
                        ProductPrice = model.CartItemViewModel.ProductPrice,
                        ImagePath = model.CartItemViewModel.ImagePath,
                        Quantity = model.CartItemViewModel.Quantity,
                    }
                );
            }
            else
            {
                CartItem.Quantity += model.CartItemViewModel.Quantity;
            }

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index", "ShoppingCart");
        }

        [HttpGet]
        public async Task<IActionResult> SearchSuggestions(string searchTerm)
        {
            var allProducts = await _productService.GetAllProduct();

            var suggestions = allProducts
                .Where(p => p.ProductName.ToLower().Contains(searchTerm.ToLower()))
                .Select(p => new
                {
                    ID = p.ID.ToString(),
                    p.ProductName
                })
                .Take(5)
                .ToList();

            return Json(suggestions);
        }

    }

}