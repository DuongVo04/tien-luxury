using MinhTienHairSalon.Models;
using MongoDB.Bson;

namespace MinhTienHairSalon.Services
{
    public interface IProductService
    {
        public Task CreateProduct(Product newProduct);
        public Task<IEnumerable<Product>> GetAllProduct();
        public Task<Product?> GetProductById(ObjectId id);
        public Task UpdateProduct(Product updatedProduct);
        public Task DeleteProduct(Product deletedProduct);
        public Task MinusQuantityInStock(ObjectId id, int quantity);
    }
}
