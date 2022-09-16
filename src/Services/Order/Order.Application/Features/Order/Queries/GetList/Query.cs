using MediatR;
using OrderModel = Order.Core.Models.Order;

namespace Order.Application.Features.Orders.Queries
{
    public class GetListQuery : IRequest<List<OrderModel>>
    {
        public string UserName { get; set; }

        public GetListQuery(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }
}