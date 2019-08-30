using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using OrderManager.Http.Dtos;

namespace OrderManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderManagerController : ControllerBase
    {
        private int _orderSeq = 0;
        private static readonly Dictionary<int, OrderDto> _orderStorage = new Dictionary<int, OrderDto>();

        /// <summary>
        /// Creates an order.
        /// </summary>
        /// <param name="dto">Order dto</param>
        /// <returns>Unique id of the created order.</returns>
        [HttpPost]
        public int CreateOrder([FromBody] CreateOrderDto dto)
        {
            int id = Interlocked.Increment(ref _orderSeq);
            _orderStorage[id] = new OrderDto
            {
                Id = id,
                Count = dto.Count,
                Item = dto.Item
            };

            return id;
        }

        /// <summary>
        /// Gets the order by the given id.
        /// </summary>
        /// <param name="orderId">The order id.</param>
        /// <returns>The Order</returns>
        [HttpGet]
        [Route("{orderId}")]
        public OrderDto GetOrder([FromRoute]int orderId)
        {
            return _orderStorage[orderId];
        }
    }
}
