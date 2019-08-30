using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrderManager.Http;
using OrderManager.Http.Dtos;
using OrderManager.Messages;
using UserManager.Dtos;

namespace UserManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private readonly IOrderManagerClient _client;
        private static readonly Dictionary<string, List<int>> _userManager = new Dictionary<string, List<int>>();
        private readonly IPublishEndpoint _publishEndpoint;

        public UserManagerController(IOrderManagerClient client, IPublishEndpoint publishEndpoint)
        {
            _client = client;
            _publishEndpoint = publishEndpoint;
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
            int orderId = await _client.CreateOrder(dto.Item, dto.Count);
            if (!_userManager.TryGetValue(userName, out List<int> orderList))
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

            OrderDto orderDto = await _client.GetOrder(lastOrderId);
            return new OrderProductDto
            {
                Count = orderDto.Count,
                Item = orderDto.Item
            };
        }

        /// <summary>
        /// Deletes the last order of the given user.
        /// </summary>
        /// <param name="userName">The user name</param>
        [HttpDelete]
        [Route("{userName}")]
        public async Task DeleteLastOrder([FromRoute] string userName)
        {
            var orderList = _userManager[userName];
            int lastOrderId = orderList.Last();
            orderList.Remove(lastOrderId);
            await _publishEndpoint.Publish(new DeleteOrder(lastOrderId));
        }
    }
}
