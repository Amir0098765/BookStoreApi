namespace BookStoreApi.Interface
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string username, string password);
        Task<bool> RegisterAsync(string userName, string password);
    }
}
