using BookStoreApi.Entities;

namespace BookStoreApi.Interface
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
