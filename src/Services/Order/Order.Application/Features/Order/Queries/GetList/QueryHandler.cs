using AutoMapper;
using MediatR;
using Order.Application.Contracts.Infrastructure;
using OrderModel = Order.Core.Models.Order;

namespace Order.Application.Features.Orders.Queries
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetListQuery, List<OrderModel>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<OrderModel>> Handle(GetListQuery request, 
            CancellationToken cancellationToken)
        {
            var orderList = await _orderRepository.GetOrdersByUserName(request.UserName);
            return _mapper.Map<List<OrderModel>>(orderList);
        }
    }
}