using Order.Core.Models;

namespace Order.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> Send(Email email);
    }
}