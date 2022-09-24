using Shop.Aggregator.Extensions;
using Shop.Aggregator.Models;

namespace Shop.Aggregator.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;

        public CatalogService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var response = await _client.GetAsync("/api/v1/Product");
            return await response.ReadContentAs<List<Product>>();
        }

        public async Task<Product> GetProduct(string id)
        {
            var response = await _client.GetAsync($"/api/v1/Product/{id}");
            return await response.ReadContentAs<Product>();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            var response = await _client.GetAsync($"/api/v1/Product/GetProductByCategory/{category}");
            return await response.ReadContentAs<List<Product>>();
        }
    }
}