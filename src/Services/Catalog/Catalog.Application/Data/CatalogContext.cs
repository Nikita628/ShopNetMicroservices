using Catalog.Application.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace Catalog.Application.Data
{
    public class CatalogContext : ICatalogContext 
    {
        public CatalogContext(IConfiguration configuration)
        {
            var settings = configuration.GetRequiredSection("DatabaseSettings").Get<DatabaseSettings>();
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Products = database.GetCollection<Product>(settings.CollectionName);
            
            DbSeed.SeedProducts(Products);
        }
        
        public IMongoCollection<Product> Products { get; set; }
    }
}