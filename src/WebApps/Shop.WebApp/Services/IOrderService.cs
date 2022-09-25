using AspnetRunBasics.Models;

namespace AspnetRunBasics.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    }

}
