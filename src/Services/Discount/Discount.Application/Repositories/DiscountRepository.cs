using Dapper;
using Discount.Application.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Application.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly DatabaseSettings _dbSettings;

        public DiscountRepository(IConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _dbSettings = configuration.GetRequiredSection("DatabaseSettings").Get<DatabaseSettings>();
        }

        public async Task<Coupon> Get(string productName)
        {
            using var connection = new NpgsqlConnection(_dbSettings.ConnectionString);

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(
                DiscountSql.SelectByProductName,
                new { ProductName = productName }
                );

            if (coupon == null)
            {
                return new Coupon
                {
                    ProductName = "No Discount",
                    Amount = 0,
                    Description = "No Discount Desc"
                };
            }

            return coupon;
        }

        public async Task<bool> Create(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_dbSettings.ConnectionString);

            var newCoupon = new Coupon
            {
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount,
            };

            var affected = await connection.ExecuteAsync(DiscountSql.Create, newCoupon);

            if (affected == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Update(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_dbSettings.ConnectionString);

            var newCoupon = new Coupon
            {
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount,
            };

            var affected = await connection.ExecuteAsync(DiscountSql.Update, newCoupon);

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> Delete(string productName)
        {
            using var connection = new NpgsqlConnection(_dbSettings.ConnectionString);

            var param = new { ProductName = productName };

            var affected = await connection.ExecuteAsync(DiscountSql.Delete, param);

            if (affected == 0)
                return false;

            return true;
        }
    }
}