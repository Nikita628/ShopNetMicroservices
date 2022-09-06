using Catalog.Api.Data.Models;
using MongoDB.Driver;

namespace Catalog.Api.Data.Contracts
{
    public interface ICatalogContext 
    {
        IMongoCollection<Product> Products { get; set; }
    }
}