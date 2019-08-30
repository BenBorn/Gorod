using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using OrderManager.Messages;
using OrderManager.Services;

namespace OrderManager.Consumers
{
    public class DeleteOrderConsumer : IConsumer<IDeleteOrder>
    {
        private readonly ILogger<DeleteOrderConsumer> _logger;
        private readonly OrderManagerService _service;

        public DeleteOrderConsumer(ILogger<DeleteOrderConsumer> logger, OrderManagerService service)
        {
            _logger = logger;
            _service = service;
        }

        public Task Consume(ConsumeContext<IDeleteOrder> context)
        {
            IDeleteOrder message = context.Message;
            _service.RemoveOrder(message.Id);

            _logger.LogInformation($"Order deleted: {message.Id}");
            return Task.CompletedTask;
        }
    }
}
