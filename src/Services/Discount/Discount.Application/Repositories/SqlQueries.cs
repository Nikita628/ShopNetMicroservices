namespace Discount.Application.Repository
{
    public static class DiscountSql
    {
        public static string SelectByProductName = "SELECT * FROM coupon WHERE ProductName = @ProductName";
        public static string Update = "UPDATE coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id";
        public static string Delete = "DELETE FROM coupon WHERE ProductName = @ProductName";
        public static string Create = "INSERT INTO coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)";
    }
}
