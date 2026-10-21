namespace BookStoreApi.Services
{
    public enum CreateOrderResult
    {
        Success,
        BookNotFound,
        InsufficientStock,
        ConcurrencyError
    }
}
