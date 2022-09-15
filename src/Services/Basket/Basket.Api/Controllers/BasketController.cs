//using AutoMapper;
using Basket.Application.Models;
//using Basket.API.GrpcServices;
using Basket.Application.Repositories;
using Basket.Application.Services;
//using EventBus.Messages.Events;
//using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using BasketModel = Basket.Application.Models.Basket;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly DiscountGrpcService _discountGrpcService;
        // private readonly IPublishEndpoint _publishEndpoint;
        // private readonly IMapper _mapper;

        public BasketController(IBasketRepository repository, DiscountGrpcService discountGrpcService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
            // _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            // _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(BasketModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketModel>> GetBasket(string userName)
        {
            var basket = await _repository.Get(userName);
            return Ok(basket ?? new BasketModel(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(BasketModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketModel>> UpdateBasket([FromBody] BasketModel basket)
        {
            foreach (var item in basket.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }

            return Ok(await _repository.Update(basket));
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _repository.Delete(userName);
            return Ok();
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            // get existing basket with total price 
            // Create basketCheckoutEvent -- Set TotalPrice on basketCheckout eventMessage
            // send checkout event to rabbitmq
            // remove the basket

            // get existing basket with total price
            var basket = await _repository.Get(basketCheckout.UserName);
            if (basket == null)
            {
                return BadRequest();
            }

            // send checkout event to rabbitmq
            //var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            //eventMessage.TotalPrice = basket.TotalPrice;
            //await _publishEndpoint.Publish(eventMessage);

            // remove the basket
            await _repository.Delete(basket.UserName);

            return Accepted();
        }
    }
}