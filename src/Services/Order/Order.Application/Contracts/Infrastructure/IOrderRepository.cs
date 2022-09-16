using OrderModel = Order.Core.Models.Order;

namespace Order.Application.Contracts.Infrastructure
{
    public interface IOrderRepository : IRepository<OrderModel>
    {
        Task<IEnumerable<OrderModel>> GetOrdersByUserName(string userName);
    }
}