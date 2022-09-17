using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderModel = Order.Infrastructure.Database.Models.Order;

namespace Order.Infrastructure.Database
{
    public class OrderContextSeed
    {
        public static void SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            orderContext.Database.Migrate();
            
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                orderContext.SaveChanges();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<OrderModel> GetPreconfiguredOrders()
        {
            return new List<OrderModel>
            {
                new OrderModel() 
                {
                    UserName = "nik", 
                    FirstName = "Admin", 
                    LastName = "Admin", 
                    EmailAddress = "admin@gmail.com", 
                    AddressLine = "Zimbabve DC", 
                    Country = "Algola", 
                    TotalPrice = 350,
                    CardName = "card name",
                    CardNumber = "1234 1234 1234 1234",
                    CreatedBy = "nik",
                    CreatedDate = DateTime.UtcNow,
                    CVV = "123",
                    Expiration = "3 days",
                    PaymentMethod = 1,
                    State = "NY",
                    ZipCode = "12345",
                }
            };
        }
    }
}