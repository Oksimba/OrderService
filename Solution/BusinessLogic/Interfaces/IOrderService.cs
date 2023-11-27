using Entities;
namespace BusinessLogic.Interfaces
{
    public interface IOrderService
    {
        public IEnumerable<Order> GetAll(int userId);

        public Task<Order> Get(int id);

        public Task Create(Order order);

        public Task Update(int id, Order updatedOrder);

        public Task<Order> Delete(int id);

        public bool IsUserOrderOwner(int userId, int orderId);

    }
}
