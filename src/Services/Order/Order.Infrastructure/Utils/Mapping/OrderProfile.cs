using AutoMapper;

namespace Order.Infrastructure.Utils.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order.Core.Models.Order, Order.Infrastructure.Database.Models.Order>().ReverseMap();
        }
    }
}