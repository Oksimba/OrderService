using BusinessLogic.Interfaces;
using Common.Helpers;
using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class DbInitializeService: IDbInitializeService
    {
        public OrderAPIDbContext Ctx { get; private set; }
        public DbInitializeService(OrderAPIDbContext ctx)
        {
            this.Ctx = ctx;
        }
        public void Initialize()
        {
            Ctx.Database.EnsureDeleted();
            Ctx.Database.Migrate();
            SeedData();
            Ctx.SaveChanges();
        }

        private void SeedData()
        {
            RemoveData();
            SeedOrders();
        }

        private void SeedOrders()
        {
            var orders = new List<Order>();
            for (int i = 0; i < 9; i++)
            {
                orders.Add(
                    new Order
                    {
                        UserId = i,
                        CardId = i,
                        OpenPrice = 30000,
                        Status = true,
                        Symbol = "BTCUSD, Bitcoin vs US Dollar",
                        Type = true,
                        Volume = 1

                    });
            }

            Ctx.Orders.AddRange(orders);
            Ctx.SaveChanges();
        }
        
        private void RemoveData()
        {
            Ctx.RemoveRange(Ctx.Orders);
            Ctx.SaveChanges();
        }
    }
}
