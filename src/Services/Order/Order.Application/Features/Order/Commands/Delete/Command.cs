using MediatR;

namespace Order.Application.Features.Orders.Commands
{
    public class DeleteCommand : IRequest
    {
        public int Id { get; set; }
    }
}