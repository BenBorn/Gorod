
namespace OrderManager.Http.Dtos
{
    /// <summary>
    /// Dto to create an order.
    /// </summary>
    public class CreateOrderDto
    {
        /// <summary>
        /// Order Item.
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// Number of items to order.
        /// </summary>
        public int Count { get; set; }
    }
}
