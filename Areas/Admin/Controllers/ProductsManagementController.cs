using System.Drawing.Printing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.DotNet.Scaffolding.Shared.Project;
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
    public class ProductsManagementController(IProductService _productService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> allProducts = await _productService.GetAllProduct();

            ProductByTypeListViewModel model = new ProductByTypeListViewModel
            {
                Hair = new List<Product>(),
                SkinCare = new List<Product>(),
                Others = new List<Product>()
            };

            foreach (var p in allProducts)
            {
                if (p.ProductType == "Mỹ phẩm tóc")
                {
                    model.Hair.Add(p); // Dùng Add() thay vì Append()
                }
                else
                {
                    if (p.ProductType == "Skin care")
                    {
                        model.SkinCare.Add(p); // Dùng Add() thay vì Append()
                    }
                    else
                    {
                        model.Others.Add(p); // Dùng Add() thay vì Append()
                    }
                }

            }

            return View(model);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        public string GetImagePath(IFormFile productImage)
        {
            try
            {
                // Tạo thư mục nếu chưa có
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image/product-image");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Tạo tên file duy nhất để tránh trùng lặp
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(productImage.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Lưu file vào thư mục
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    productImage.CopyTo(fileStream);
                }

                // Gán đường dẫn vào thuộc tính ImagePath
                return "/image/product-image/" + uniqueFileName;
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductAddEditViewMode model, IFormFile productImage)
        {
            if (ModelState.IsValid)
            {

                model.Product.ImagePath = GetImagePath(productImage);

                await _productService.CreateProduct(model.Product);

                // return RedirectToAction("Index");
                return Json(new { success = true, RedirectUrl = Url.Action("Index", "ProductsManagement") });

            }

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());

            return Json(new { success = false, errors });

        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(ObjectId id)
        {
            ProductAddEditViewMode model = new()
            {
                Product = await _productService.GetProductById(id)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductAddEditViewMode model, IFormFile productImage)
        {
            if (model.Product.ImagePath == null)
            {

                if (ModelState.IsValid)
                {
                    model.Product.ImagePath = GetImagePath(productImage);
                    await _productService.UpdateProduct(model.Product);

                    // return RedirectToAction("Index");
                    return Json(new { success = true, RedirectUrl = Url.Action("Index", "ProductsManagement") });
                }
            }
            else
            {
                if (productImage == null && model.Product.ImagePath != null)
                {
                    await _productService.UpdateProduct(model.Product);
                    return Json(new { success = true, RedirectUrl = Url.Action("Index", "ProductsManagement") });
                }

                if (!string.IsNullOrEmpty(model.Product.ProductName))
                {
                    model.Product.ImagePath = GetImagePath(productImage);
                    await _productService.UpdateProduct(model.Product);

                    return Json(new { success = true, RedirectUrl = Url.Action("Index", "ProductsManagement") });
                }
                else
                {
                    return Json(new { success = false, Message = "Tên sản phẩm không được để trống" });
                }

            }

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());

            return Json(new { success = false, errors });
        }

        public async Task<IActionResult> DeleteProduct(ObjectId id)
        {
            await _productService.DeleteProduct(await _productService.GetProductById(id));

            return RedirectToAction("Index");
        }
    }
}
