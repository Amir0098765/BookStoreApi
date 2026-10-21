namespace BookStoreApi.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int Stock { get; set; }
        public byte[] RowVersion { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
