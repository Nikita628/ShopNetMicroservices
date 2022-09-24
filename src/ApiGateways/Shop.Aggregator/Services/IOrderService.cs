using Shop.Aggregator.Models;

namespace Shop.Aggregator.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    }
}