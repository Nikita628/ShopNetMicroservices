
namespace Shop.Aggregator.Models
{
    public class Shopping
    {
        public string UserName { get; set; }
        public Basket BasketWithProducts { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}