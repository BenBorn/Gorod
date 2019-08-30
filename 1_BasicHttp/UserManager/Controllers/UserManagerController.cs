using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserManager.Dtos;

namespace UserManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private IHttpClientFactory _httpClientFactory;
        private static Dictionary<string, List<int>> _userManager = new Dictionary<string, List<int>>();

        public UserManagerController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Creates an order for the given user.
        /// </summary>
        /// <param name="userName">The user which creates the order.</param>
        /// <param name="dto">The product to order.</param>
        [HttpPost]
        [Route("{userName}")]
        public async Task CreateOrder([FromRoute] string userName, [FromBody]OrderProductDto dto)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001/");
            var requestDto = new CreateOrderDto
            {
                Count = dto.Count,
                Item = dto.Item
            };
            HttpResponseMessage response = await client.PostAsJsonAsync("api/OrderManager", requestDto);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Order failed");
            }

            int orderId = Convert.ToInt32(await response.Content.ReadAsStringAsync());
            List<int> orderList;
            if (!_userManager.TryGetValue(userName, out orderList))
            {
                orderList = new List<int>();
                _userManager[userName] = orderList;
            }
            orderList.Add(orderId);
        }

        /// <summary>
        /// Returns the last order of the given user.
        /// </summary>
        /// <param name="userName">The user name</param>
        /// <returns>The last order.</returns>
        [HttpGet]
        [Route("{userName}")]
        public async Task<OrderProductDto> GetLastOrder([FromRoute] string userName)
        {
            int lastOrderId = _userManager[userName].Last();

            HttpClient client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001/");

            HttpResponseMessage response = await client.GetAsync($"api/OrderManager/{lastOrderId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to select order");
            }
            var orderDto = await response.Content.ReadAsAsync<CreateOrderDto>();
            return new OrderProductDto
            {
                Count = orderDto.Count,
                Item = orderDto.Item
            };
        }
    }
}
