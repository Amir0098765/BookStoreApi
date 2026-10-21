using BookStoreApi.DTOs;
using BookStoreApi.Interface;
using Microsoft.AspNetCore.Mvc;
using BookStoreApi.Data;
using BookStoreApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using BookStoreApi.Repositories;
namespace BookStoreApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMessageService _messageService;
        private readonly BookStoreDbContext _context;
        private readonly IOrderRepository _orderRepository;

        public OrderService(IMessageService messageService , BookStoreDbContext context , IOrderRepository orderRepository)
        {
            this._messageService = messageService;
            this._context = context;
            this._orderRepository = orderRepository;
        }
        public async Task<CreateOrderResult> CreateOrder(CreateOrderRequest request , int userId)
        {
            var book = await _context.Books.FindAsync(request.BookId);
            if (book == null)
                return CreateOrderResult.BookNotFound;
            if (book.Stock < request.Quantity)
                return CreateOrderResult.InsufficientStock;
            var order = new Order
            {
                BookId = request.BookId,
                Quantity = request.Quantity,
                UserId = userId
            };
            book.Stock -= request.Quantity;
            _context.Orders.Add(order);
            for (int i = 0;i < 3; i++)
            {
                try
                {
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Order Created");
                    Console.WriteLine(request.BookId);
                    Console.WriteLine(request.Quantity);
                    return CreateOrderResult.Success;
                }
                catch (DbUpdateConcurrencyException)
                {
                    await _context.Entry(book).ReloadAsync();
                    if (book.Stock < request.Quantity)
                        return CreateOrderResult.InsufficientStock;
                    book.Stock -= request.Quantity;

                }
            }
            return CreateOrderResult.ConcurrencyError;
        }

        public async Task<List<Order>> GetOrdersByUserId (int userId)
        {
            return await _orderRepository.GetOrdersByUserId(userId);
        }

    }
}
