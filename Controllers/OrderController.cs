using BookStoreApi.DTOs;
using BookStoreApi.Entities;
using BookStoreApi.Interface;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            Console.WriteLine("order controller created");
            this._orderService = orderService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = await _orderService.CreateOrder(request , userId);
            if (result == CreateOrderResult.BookNotFound)
            {
                return NotFound("Book not found");
            }
            if (result == CreateOrderResult.InsufficientStock)
            {
                return BadRequest("Insufficient stock");
            }
            if (result == CreateOrderResult.ConcurrencyError)
            {
                return Conflict("Please try again");
            }
            return Ok("Order created");



        }
        [HttpGet]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var orders = await _orderService.GetOrdersByUserId(userId);

            return Ok(orders);
        }

    }
}
