using BasketModel = Basket.Application.Models.Basket;

namespace Basket.Application.Repositories
{
    public interface IBasketRepository
    {
        Task<BasketModel> Get(string userName);
        Task<BasketModel> Update(BasketModel basket);
        Task Delete(string userName);
    }
}