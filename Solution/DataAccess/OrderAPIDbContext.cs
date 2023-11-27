using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class OrderAPIDbContext : DbContext
    {
        public OrderAPIDbContext(DbContextOptions<OrderAPIDbContext> options) : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        
    }
}
