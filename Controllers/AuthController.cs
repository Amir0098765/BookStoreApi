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

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
             _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.LoginAsync(
                request.UserName,
                request.Password);

            if (token == null)
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request.UserName, request.Password);

            if (!result)
                return BadRequest("Username already exists");

            return Ok("User registered successfully");
        }

    }
}
