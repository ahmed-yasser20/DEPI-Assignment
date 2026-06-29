using BookStoreAPI.Models.Entities;

namespace BookStoreAPI.Services.Interfaces;

public interface IJwtService
{
    string GenerateToken(AppUser user, IList<string> roles);
    DateTime GetExpiry();
}
