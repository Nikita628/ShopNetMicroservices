﻿using AspnetRunBasics.Extensions;
using AspnetRunBasics.Models;

namespace AspnetRunBasics.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _client;

        public OrderService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            var response = await _client.GetAsync($"/Order/{userName}");
            return await response.ReadContentAs<List<Order>>();
        }
    }
}
