using BookStoreAPI.Models.DTOs.Orders;

namespace BookStoreAPI.Services.Interfaces;

public interface IOrderService
{
    Task<OrderResponse> CreateOrderAsync(string userId, CreateOrderRequest request);
    Task<List<OrderResponse>> GetMyOrdersAsync(string userId);
    Task<List<OrderResponse>> GetAllOrdersAsync();
    Task<OrderResponse?> GetOrderByIdAsync(int id, string? userId = null);
}
