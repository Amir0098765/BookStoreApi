using BookStoreApi.Configuration;
using BookStoreApi.Data;
using BookStoreApi.Data;
using BookStoreApi.DTOs;
using BookStoreApi.Entities;
using BookStoreApi.Entities;
using BookStoreApi.Interface;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace BookStoreApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly BookStoreDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;



        public AuthService (BookStoreDbContext context ,IPasswordHasher<User> passwordHasher ,IJwtTokenService jwtTokenService )
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;

        }

        public async Task<string> LoginAsync(string userName , string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
            {
                return null;
            }

            var result = _passwordHasher.VerifyHashedPassword(user,user.Password,password);

            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return _jwtTokenService.GenerateToken(user);
        }

        public async Task<bool> RegisterAsync(string userName,string password)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == userName);

            if (existingUser != null)
            {
                return false;
            }

            var user = new User
            {
                UserName = userName,
                Password = ""
            };

            user.Password = _passwordHasher.HashPassword(
                user,
                password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
