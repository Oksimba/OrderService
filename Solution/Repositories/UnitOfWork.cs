using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repositories.Interfaces;

namespace DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private readonly OrderAPIDbContext _context;
        private readonly IOrderRepository _orderRepository;


        public UnitOfWork(OrderAPIDbContext context,
            IOrderRepository orderRepository)
        {
            _context = context;
            _orderRepository = orderRepository;
        }

        public IOrderRepository OrderRepository => _orderRepository;



        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool EnsureDataBaseDeleted()
        {
            return _context.Database.EnsureDeleted();
        }

        public void Migrate()
        {
            _context.Database.Migrate();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
