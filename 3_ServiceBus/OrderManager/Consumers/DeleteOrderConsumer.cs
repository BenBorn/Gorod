using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using OrderManager.Messages;

namespace OrderManager.Consumers
{
    public class DeleteOrderConsumer : IConsumer<IDeleteOrder>
    {
        private readonly ILogger<DeleteOrderConsumer> _logger;

        public DeleteOrderConsumer(ILogger<DeleteOrderConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<IDeleteOrder> context)
        {
            IDeleteOrder message = context.Message;

            _logger.LogInformation($"Order deleted: {message.Id}");
            return Task.CompletedTask;
        }
    }
}
