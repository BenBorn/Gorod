

namespace OrderManager.Messages
{
    /// <summary>
    /// Event which is raised after a new order is created
    /// </summary>
    public interface IOrderCreated
    {
        /// <summary>
        /// Id of the order.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Order Item.
        /// </summary>
        string Item { get; }

        /// <summary>
        /// Number of items to order.
        /// </summary>
        int Count { get; }
    }

    public class OrderCreated : IOrderCreated
    {
        public OrderCreated(int id, string item, int count)
        {
            Id = id;
            Item = item;
            Count = count;
        }

        public int Id { get; }
        public string Item { get; }
        public int Count { get; }
    }
}
