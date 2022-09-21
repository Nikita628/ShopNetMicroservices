using AutoMapper;
using Order.Application.Features.Orders.Commands;

namespace Order.Application.Utils.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CheckoutCommand, Core.Models.Order>().ReverseMap();
        }
    }
}