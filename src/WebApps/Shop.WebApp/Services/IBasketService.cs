using AspnetRunBasics.Models;

namespace AspnetRunBasics.Services
{
    public interface IBasketService
    {
        Task<Basket> GetBasket(string userName);
        Task<Basket> UpdateBasket(Basket model);
        Task CheckoutBasket(BasketCheckout model);
    }
}
