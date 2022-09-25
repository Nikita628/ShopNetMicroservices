using Shop.WebApp.Models;

namespace Shop.WebApp.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    }

}
