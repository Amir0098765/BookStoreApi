using System.Text.Json.Serialization;

namespace BookStoreApi.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }

        public int UserId { get; set; }
        [JsonIgnore]
        public User user { get; set; }

    }
}
