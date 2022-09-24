using Shop.Aggregator.Models;

namespace Shop.Aggregator.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetProductsByCategory(string category);
        Task<Product> GetProduct(string id);
    }
}