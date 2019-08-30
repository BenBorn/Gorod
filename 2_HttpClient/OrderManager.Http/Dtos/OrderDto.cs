namespace OrderManager.Http.Dtos
{
    /// <summary>
    /// An Order
    /// </summary>
    public class OrderDto
    {
        /// <summary>
        /// Unique Id of the order.
        /// </summary>
        public int Id { get; set; }

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
