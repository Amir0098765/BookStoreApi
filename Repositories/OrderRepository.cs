using BookStoreApi.Entities;
using BookStoreApi.Data;
using Microsoft.EntityFrameworkCore;
namespace BookStoreApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BookStoreDbContext _context;
        public OrderRepository(BookStoreDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetOrdersByUserId(int userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
        }
    }
}
