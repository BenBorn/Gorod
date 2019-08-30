using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using OrderManager.Http.Dtos;
using OrderManager.Messages;

namespace OrderManager.Services
{
    public class OrderManagerService
    {
        private static int _orderSeq = 0;
        private static readonly Dictionary<int, OrderDto> _orderStorage = new Dictionary<int, OrderDto>();
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderManagerService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task<int> CreateOrder(CreateOrderDto dto)
        {
            int id = Interlocked.Increment(ref _orderSeq);
            _orderStorage[id] = new OrderDto
            {
                Id = id,
                Count = dto.Count,
                Item = dto.Item
            };

            await _publishEndpoint.Publish(new OrderCreated(id, dto.Item, dto.Count));

            return id;
        }

        public OrderDto GetOrder(int orderId)
        {
            return _orderStorage[orderId];
        }

        public void RemoveOrder(int orderId)
        {
            _orderStorage.Remove(orderId);
        }
    }
}
