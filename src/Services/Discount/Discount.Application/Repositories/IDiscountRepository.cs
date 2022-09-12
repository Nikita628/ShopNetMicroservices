using Discount.Application.Models;
using System.Threading.Tasks;

namespace Discount.Application.Repository
{
    public interface IDiscountRepository
    {
        Task<Coupon> Get(string productName);

        Task<bool> Create(Coupon coupon);
        Task<bool> Update(Coupon coupon);
        Task<bool> Delete(string productName);
    }
}