
namespace UserManager.Dtos
{
    /// <summary>
    /// Dto of a product order.
    /// </summary>
    public class OrderProductDto
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
