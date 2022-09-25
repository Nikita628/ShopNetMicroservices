using Shop.WebApp.Extensions;
using Shop.WebApp.Models;

namespace Shop.WebApp.Services
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
            var response = await _client.GetAsync("/Product");
            return await response.ReadContentAs<List<Product>>();
        }

        public async Task<Product> GetProduct(string id)
        {
            var response = await _client.GetAsync($"/Product/{id}");
            return await response.ReadContentAs<Product>();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            var response = await _client.GetAsync($"/Product/GetProductByCategory/{category}");
            return await response.ReadContentAs<List<Product>>();
        }

        public async Task<Product> CreateProduct(Product model)
        {
            var response = await _client.PostAsJson($"/Product", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<Product>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}
