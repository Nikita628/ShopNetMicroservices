using Microsoft.AspNetCore.Mvc;
using Shop.Aggregator.Services;
using System.Net;
using ShopModel = Shop.Aggregator.Models.Shopping;

namespace Shop.Aggregator.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ShopController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public ShopController(ICatalogService catalogService, IBasketService basketService, IOrderService orderService)
        {
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        [HttpGet("{userName}", Name = "GetShop")]
        [ProducesResponseType(typeof(ShopModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShopModel>> GetShop(string userName)
        {
            var basket = await _basketService.GetBasket(userName);

            foreach (var item in basket.Items)
            {
                var product = await _catalogService.GetProduct(item.ProductId);

                item.ProductName = product.Name;
                item.Category = product.Category;
                item.Summary = product.Summary;
                item.Description = product.Description;
                item.ImageFile = product.ImageFile;
            }

            var orders = await _orderService.GetOrdersByUserName(userName);

            var ShopModel = new ShopModel
            {
                UserName = userName,
                BasketWithProducts = basket,
                Orders = orders
            };

            return Ok(ShopModel);
        }
    }
}