using TienLuxury.Models;

namespace TienLuxury.ViewModels
{
    public class ProductDetailViewModel
    {
        public Product? Product { get; set; }
        public List<Product>? Products { get; set; }

        public CartItemViewModel? CartItemViewModel { get; set; }
    }
}