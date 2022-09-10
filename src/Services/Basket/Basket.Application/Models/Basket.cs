using System.Collections.Generic;

namespace Basket.Application.Models
{
    public class Basket
    {
        public string UserName { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

        public Basket()
        {
        }

        public Basket(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice
        {
            get
            {
                return Items.Aggregate(0.0m, (accumulator, item) => {
                    return accumulator + (item.Price * item.Quantity);
                });
            }
        }
    }
}