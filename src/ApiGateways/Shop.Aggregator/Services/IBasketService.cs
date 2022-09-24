using Shop.Aggregator.Models;

namespace Shop.Aggregator.Services
{
    public interface IBasketService
    {
        Task<Basket> GetBasket(string userName);
    }
}