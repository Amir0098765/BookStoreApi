using System.ComponentModel.DataAnnotations;
namespace BookStoreApi.DTOs
{
    public class CreateOrderRequest
    {
        //[Range(1 , int.MaxValue , ErrorMessage = "quantity must be at least 1")]
        public int BookId { get; set; }
        //[Range(1 , int.MaxValue , ErrorMessage = "Book id bust be at least 1")]
        public int Quantity { get; set; }
    }
}
