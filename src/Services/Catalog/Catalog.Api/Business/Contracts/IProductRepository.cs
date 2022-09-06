using Catalog.Api.Data.Models;

namespace Catalog.Api.Business.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetList();
        Task<Product> Get(string id);
        Task<IEnumerable<Product>> GetByName(string name);
        Task<IEnumerable<Product>> GetByCategory(string categoryName);

        Task Create(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(string id);
    }
}