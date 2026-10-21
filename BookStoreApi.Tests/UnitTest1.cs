using BookStoreApi.DTOs;
using BookStoreApi.Validators;
using Xunit;
namespace BookStoreApi.Tests
{
    public class RegisterRequestValidatorTests
    {
        [Fact]
        public void Register_ShouldFail_WhenUsernameIsTooShort()
        {
            var validator = new RegisterRequestValidator();

            var request = new RegisterRequest
            {
                UserName = "ab",
                Password = "ValidPass123",
            };

            var result = validator.Validate(request);

            Assert.False(result.IsValid);
        }
        [Fact]
        public void Register_ShouldPass_WhenRequestIsValid()
        {
            var validator = new RegisterRequestValidator();

            var request = new RegisterRequest
            {
                UserName = "Amir123",
                Password = "ValidPass123"
            };

            var result = validator.Validate(request);

            Assert.True(result.IsValid);
        }
        [Fact]
        public void Register_ShouldFail_WhenPasswordIsTooShort()
        {
            var validator = new RegisterRequestValidator();

            var request = new RegisterRequest
            {
                UserName = "Amir123",
                Password = "123"
            };

            var result = validator.Validate(request);

            Assert.False(result.IsValid);
        }
    }
}
