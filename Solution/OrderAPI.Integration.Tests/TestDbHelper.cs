using Common.Helpers;
using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace OrderAPI.Integration.Tests
{
    internal class TestDbHelper
    {
        private static OrderAPIDbContext Context { get; set; }

        public TestDbHelper(OrderAPIDbContext context)
        {
            Context = context;
        }
        public void CreateTestDb()
        {
            Context.Database.Migrate();
            SeedData();
        }

        private static void SeedData()
        {
            RemoveData();
            SeedOrders();
            Context.SaveChanges();
        }


        private static void SeedOrders()
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

            Context.Orders.AddRange(orders);
        }
        private static void RemoveData()
        {
            Context.RemoveRange(Context.Orders);
            Context.SaveChanges();
        }

    }
}
