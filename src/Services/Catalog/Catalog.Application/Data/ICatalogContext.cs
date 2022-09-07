using MongoDB.Driver;

namespace Catalog.Application.Data
{
    public interface ICatalogContext 
    {
        IMongoCollection<Product> Products { get; set; }
    }
}