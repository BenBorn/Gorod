using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderManager.Http.Dtos;
using OrderManager.Services;

namespace OrderManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderManagerController : ControllerBase
    {
        private readonly OrderManagerService _service;

        public OrderManagerController(OrderManagerService service)
        {
            _service = service;
        }

        /// <summary>
        /// Creates an order.
        /// </summary>
        /// <param name="dto">Order dto</param>
        /// <returns>Unique id of the created order.</returns>
        [HttpPost]
        public Task<int> CreateOrder([FromBody] CreateOrderDto dto)
        {
            return _service.CreateOrder(dto);
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
            return _service.GetOrder(orderId);
        }
    }
}
