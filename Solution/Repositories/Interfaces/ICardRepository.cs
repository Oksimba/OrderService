using Entities;

namespace Repositories.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll(int userId);
        Task<Order> Get(int id);
        Task Create(Order orders);
        Task CreateMany(IEnumerable<Order> orders);
        Task Update(int id, Order order);
        Task<Order> Delete(int id);
        Task DeleteMany(IEnumerable<Order> orders);

        bool IsUserOrderOwner(int userId, int orderId);
    }
}
