using DataAccess;
using Entities;
using Repositories.Interfaces;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private OrderAPIDbContext context;

        public OrderRepository(OrderAPIDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Order> GetAll(int userId)
        {
            return context.Orders.Where(o => o.UserId == userId);
        }

        public async Task<Order> Get(int id)
        {
            return await context.Orders.FindAsync(id);
        }

        public async Task Create(Order order)
        {
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
        }

        public async Task CreateMany(IEnumerable<Order> orders)
        {
            await context.Orders.AddRangeAsync(orders);
            await context.SaveChangesAsync();
        }

        public async Task Update(int id, Order order)
        {
            Order currentOrder = await Get(id);
            currentOrder.Profit = order.Profit;
            currentOrder.Status = order.Status;
            currentOrder.OpenPrice = order.OpenPrice;
            currentOrder.ClosePrice = order.ClosePrice;
            context.Orders.Update(currentOrder);
            await context.SaveChangesAsync();
        }

        public async Task<Order> Delete(int id)
        {
            Order order = await Get(id);
            if (order != null)
            {
                context.Orders.Remove(order);
                await context.SaveChangesAsync();
            }
            return order;
        }

        public async Task DeleteMany(IEnumerable<Order> orders)
        {
            if (orders != null)
            {
                context.Orders.RemoveRange(orders);
                await context.SaveChangesAsync();
            }
        }

        public bool IsUserOrderOwner(int userId, int orderId)
        {
            var order = context.Orders.FirstOrDefault(o => o.Id == orderId);

            return order != null && order.UserId == userId;
        }
    }
}
