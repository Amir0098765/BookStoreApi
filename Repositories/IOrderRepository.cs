using BookStoreApi.Entities;
namespace BookStoreApi.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrdersByUserId(int userId);
    }
}
