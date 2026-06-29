using BookStoreAPI.Models.DTOs.Auth;

namespace BookStoreAPI.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}
