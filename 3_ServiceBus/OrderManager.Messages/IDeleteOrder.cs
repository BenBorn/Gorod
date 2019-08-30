
namespace OrderManager.Messages
{
    /// <summary>
    /// Command to delete an order.
    /// </summary>
    public interface IDeleteOrder
    {
        /// <summary>
        /// Id of the order which should be deleted.
        /// </summary>
        int Id { get; }
    }

    public class DeleteOrder : IDeleteOrder
    {
        public DeleteOrder(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
