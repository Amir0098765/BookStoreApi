using System;
using System.Collections.Generic;
using System.Text;
using BookStoreApi.DTOs;
using BookStoreApi.Validators;
using Xunit;

namespace BookStoreApi.Tests
{
    public class LoginRequestValidatorTests
    {
        [Fact]
        public void Login_ShouldFail_WhenUsernameIsEmpty()
        {
            var validator = new LoginRequestValidator();
            var request = new LoginRequest
            {
                UserName = "",
                Password = "password1234"
            };

            var result = validator.Validate(request);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Login_ShouldFail_WhenPasswordIsEmpty()
        {
            var validator = new LoginRequestValidator();
            var request = new LoginRequest
            {
                UserName = "password1234",
                Password = ""
            };

            var result = validator.Validate(request);

            Assert.False(result.IsValid);
        }
    }
}
