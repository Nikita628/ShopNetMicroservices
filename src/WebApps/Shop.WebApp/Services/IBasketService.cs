using Shop.WebApp.Models;

namespace Shop.WebApp.Services
{
    public interface IBasketService
    {
        Task<Basket> GetBasket(string userName);
        Task<Basket> UpdateBasket(Basket model);
        Task CheckoutBasket(BasketCheckout model);
    }
}
