using AutoMapper;

namespace Basket.Api.Mapping
{
    public class CheckoutProfile : Profile
    {
        public CheckoutProfile()
        {
            CreateMap<Basket.Application.Models.BasketCheckout, EventBus.Events.BasketCheckoutEvent>();
        }
    }
}