namespace Discount.Application.Repository
{
    public static class DiscountSql
    {
        public static string SelectByProductName = "SELECT * FROM Coupon WHERE ProductName = @ProductName";
        public static string Update = "UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id";
        public static string Delete = "DELETE FROM Coupon WHERE ProductName = @ProductName";
        public static string Create = "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)";
    }
}
