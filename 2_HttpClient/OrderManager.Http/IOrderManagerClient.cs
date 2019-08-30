
using System;
using System.Net.Http;
using System.Threading.Tasks;
using OrderManager.Http.Dtos;

namespace OrderManager.Http
{
    /// <summary>
    /// Client to access the OrderManager service from other services.
    /// </summary>
    public interface IOrderManagerClient
    {
        /// <summary>
        /// Creates an order.
        /// </summary>
        /// <param name="item">Product item</param>
        /// <param name="count">Number of items.</param>
        /// <returns>Unique id of the created order.</returns>
        Task<int> CreateOrder(string item, int count);

        /// <summary>
        /// Gets the order by the given id.
        /// </summary>
        /// <param name="orderId">The order id.</param>
        /// <returns>The Order</returns>
        Task<OrderDto> GetOrder(int orderId);
    }

    class OrderManagerClient : IOrderManagerClient
    {
        private readonly HttpClient _client;

        public OrderManagerClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<int> CreateOrder(string item, int count)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/OrderManager", new CreateOrderDto
            {
                Count = count,
                Item = item
            });
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Order failed");
            }

            return Convert.ToInt32(await response.Content.ReadAsStringAsync());
        }

        public async Task<OrderDto> GetOrder(int orderId)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/OrderManager/{orderId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to select order");
            }
            return await response.Content.ReadAsAsync<OrderDto>();
        }
    }
}
