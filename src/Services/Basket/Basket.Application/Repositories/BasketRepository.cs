using BasketModel = Basket.Application.Models.Basket;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Application.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task<BasketModel> Get(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);
            
            if (String.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<BasketModel>(basket);
        }

        public async Task<BasketModel> Update(BasketModel basket)
        {
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));

            return await Get(basket.UserName);
        }

        public async Task Delete(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }
    }
}