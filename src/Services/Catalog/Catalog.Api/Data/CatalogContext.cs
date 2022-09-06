using Catalog.Api.Data.Contracts;
using Catalog.Api.Data.Models;
using Catalog.Api.Data.Utils;
using Catalog.Api.Models;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogContext : ICatalogContext 
    {
        public CatalogContext(IConfiguration configuration)
        {
            var settings = configuration.GetValue<DatabaseSettings>("DatabaseSettings");
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Products = database.GetCollection<Product>(settings.CollectionName);
            
            DbSeed.SeedProducts(Products);
        }
        
        public IMongoCollection<Product> Products { get; set; }
    }
}