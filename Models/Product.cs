using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TienLuxury.Models
{
    [Collection("product")]
    public class Product
    {
        private ObjectId id;

        [Required(ErrorMessage = "Nhập tên sản phẩm")]
        [Display(Name = "Tên sản phẩm")]
        private string? productName;

        [Display(Name = "Giá")]
        private Decimal price;

        [Required(ErrorMessage = "Nhập số lượng trong kho")]
        [Display(Name = "Số lượng trong kho")]
        private int? quantityInStock;

        [Display(Name = "Mô tả")]
        private string? description;

        private string imagePath = string.Empty;
        private bool isSold;
        private string? productType;

        public ObjectId ID { get => id; set => id = value; }
        public string? ProductName { get => productName; set => productName = value; }
        public Decimal Price { get => price; set => price = value; }
        public int? QuantityInStock { get => quantityInStock; set => quantityInStock = value; }
        public string? Description { get => description; set => description = value; }
        public string ImagePath { get => imagePath; set => imagePath = value; }
        public Boolean IsSold { get => isSold; set => isSold = value; }
        public string ProductType { get => productType; set => productType = value; }
    }
}
