using BookStoreApi.DTOs;
using BookStoreApi.Entities;
namespace BookStoreApi.Services
{
    public interface IOrderService
    {
        Task<CreateOrderResult> CreateOrder(CreateOrderRequest request , int userId);

        Task<List<Order>> GetOrdersByUserId(int userId);
    }
}
