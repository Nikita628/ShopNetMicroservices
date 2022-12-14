using Dapper;
using Discount.Application.Models;
using Npgsql;

namespace Discount.Application.Repository
{
    public static class Seed
    {
        private static string createTableQuery = @"
            CREATE TABLE coupon(
            ID              SERIAL PRIMARY KEY NOT NULL,
            ProductName     VARCHAR(24) NOT NULL,
            Description     TEXT,
            Amount          INT
	    );";

        private static string isTableExistsQuery = @"
            SELECT EXISTS (
            SELECT FROM 
                pg_tables
                WHERE 
                schemaname = 'public' AND 
                tablename  = 'coupon'
        );";

        private static string isTableEmptyQuery = @"select NOT EXISTS(SELECT 1 FROM coupon)";

        private static List<Coupon> coupons = new List<Coupon>
        {
            new Coupon
            {
                ProductName = "IPhone X",
                Description = "IPhone Discount",
                Amount = 150,
            },
            new Coupon
            {
                ProductName = "Samsung 10",
                Description = "Samsung Discount",
                Amount = 150,
            },
        };

        public static void SeedDb(DatabaseSettings settings)
        {
            using var connection = new NpgsqlConnection(settings.ConnectionString);

            var isCouponTableExists = connection.ExecuteScalar<bool>(isTableExistsQuery);

            if (!isCouponTableExists)
            {
                connection.Execute(createTableQuery);
            }

            var isCouponTableEmpty = connection.ExecuteScalar<bool>(isTableEmptyQuery);

            if (isCouponTableEmpty)
            {
                coupons.ForEach(c =>
                {
                    connection.Execute(DiscountSql.Create, c);
                });
            }
        }
    }
}