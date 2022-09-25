using AspnetRunBasics.Models;

namespace AspnetRunBasics.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetProductsByCategory(string category);
        Task<Product> GetProduct(string id);
        Task<Product> CreateProduct(Product model);
    }
}
