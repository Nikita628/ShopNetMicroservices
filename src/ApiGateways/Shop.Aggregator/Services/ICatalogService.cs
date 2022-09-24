using Shop.Aggregator.Models;

namespace Shop.Aggregator.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<Catalog>> GetCatalog();
        Task<IEnumerable<Catalog>> GetCatalogByCategory(string category);
        Task<Catalog> GetCatalog(string id);
    }
}