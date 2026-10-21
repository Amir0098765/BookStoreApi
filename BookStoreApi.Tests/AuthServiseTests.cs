using BookStoreApi.Data;
using BookStoreApi.Entities;
using BookStoreApi.Interface;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BookStoreApi.Tests
{
    public class AuthServiseTests
    {
        [Fact]
        public async Task RegisterAsync_ShouldSaveUserWithHashedPassword()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new BookStoreDbContext(options);

            var passwordHasher = new PasswordHasher<User>();
            var jwtTokenService = new FakeJwtTokenService();

            var authService = new AuthService(
                context,
                passwordHasher,
                jwtTokenService);

            // Act
            var result = await authService.RegisterAsync(
                "Amir123",
                "ValidPass123");

            // Assert
            Assert.True(result);

            var user = await context.Users
                .SingleAsync(u => u.UserName == "Amir123");

            Assert.NotEqual("ValidPass123", user.Password);

            var verification = passwordHasher.VerifyHashedPassword(
                user,
                user.Password,
                "ValidPass123");

            Assert.Equal(
                PasswordVerificationResult.Success,
                verification);
        }

        private sealed class FakeJwtTokenService : IJwtTokenService
        {
            public string GenerateToken(User user)
            {
                return "fake-token";
            }
        }
        [Fact]
        public async Task RegisterAsync_ShouldFail_WhenUsernameAlreadyExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new BookStoreDbContext(options);

            var passwordHasher = new PasswordHasher<User>();
            var jwtTokenService = new FakeJwtTokenService();

            var authService = new AuthService(
                context,
                passwordHasher,
                jwtTokenService);

            // Act
            await authService.RegisterAsync("Amir123", "ValidPass123");

            var result = await authService.RegisterAsync(
                "Amir123",
                "AnotherPass456");

            // Assert
            Assert.False(result);

            Assert.Equal(
                1,
                await context.Users.CountAsync());
        }
        [Fact]
        public async Task LoginAsync_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new BookStoreDbContext(options);

            var passwordHasher = new PasswordHasher<User>();
            var jwtTokenService = new FakeJwtTokenService();

            var authService = new AuthService(
                context,
                passwordHasher,
                jwtTokenService);

            await authService.RegisterAsync("Amir123", "ValidPass123");

            // Act
            var token = await authService.LoginAsync(
                "Amir123",
                "ValidPass123");

            // Assert
            Assert.Equal("fake-token", token);
        }

        [Fact]
        public async Task LoginAsync_ShouldFail_WhenPasswordIsIncorrect()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new BookStoreDbContext(options);

            var passwordHasher = new PasswordHasher<User>();
            var jwtTokenService = new FakeJwtTokenService();

            var authService = new AuthService(
                context,
                passwordHasher,
                jwtTokenService);

            await authService.RegisterAsync("Amir123", "ValidPass123");

            // Act
            var token = await authService.LoginAsync(
                "Amir123",
                "WrongPassword123");

            // Assert
            Assert.Null(token);
        }

        [Fact]
        public async Task LoginAsync_ShouldFail_WhenUserDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new BookStoreDbContext(options);

            var passwordHasher = new PasswordHasher<User>();
            var jwtTokenService = new FakeJwtTokenService();

            var authService = new AuthService(
                context,
                passwordHasher,
                jwtTokenService);

            // Act
            var token = await authService.LoginAsync(
                "UnknownUser",
                "ValidPass123");

            // Assert
            Assert.Null(token);
        }
    }
}
