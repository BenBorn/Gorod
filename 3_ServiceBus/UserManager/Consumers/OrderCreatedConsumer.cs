using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using OrderManager.Messages;

namespace UserManager.Consumers
{
    public class OrderCreatedConsumer : IConsumer<IOrderCreated>
    {
        private readonly ILogger<OrderCreatedConsumer> _logger;

        public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<IOrderCreated> context)
        {
            IOrderCreated message = context.Message;
            _logger.LogInformation($"Order created: {message.Id}, {message.Item}");
            return Task.CompletedTask;
        }
    }
}
