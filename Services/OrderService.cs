using BookStoreAPI.Data;
using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Services
{
    public class OrderService
    {
        private readonly AppDbContext _db;

        public OrderService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<OrderResultDto> PlaceOrder(string userId, CreateOrderDto dto)
        {
            if (dto.Items == null || dto.Items.Count == 0)
            {
                throw new Exception("The order must contain at least one item.");
            }

            decimal total = 0;
            var orderItems = new List<OrderItem>();

            foreach (var item in dto.Items)
            {
                var book = await _db.Books.FindAsync(item.BookId);

                if (book == null)
                {
                    throw new Exception($"Book {item.BookId} not found.");
                }

                if (book.Stock < item.Quantity)
                {
                    throw new Exception($"The book '{book.Title}' is not available in the requested quantity.");
                }

                book.Stock -= item.Quantity;

                total += book.Price * item.Quantity;

                orderItems.Add(new OrderItem
                {
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    Price = book.Price
                });
            }

            var order = new Order
            {
                UserId = userId,
                TotalPrice = total,
                Items = orderItems
            };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            return await GetOrderById(order.Id, userId);
        }

        public async Task<List<OrderResultDto>> GetMyOrders(string userId)
        {
            var orders = await _db.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Items)
                .ThenInclude(i => i.Book)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return orders.Select(o => MapToDto(o)).ToList();
        }

        public async Task<List<OrderResultDto>> GetAllOrders()
        {
            var orders = await _db.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Book)
                .Include(o => o.User)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return orders.Select(o => MapToDto(o)).ToList();
        }

        public async Task<OrderResultDto> GetOrderById(int orderId, string? userId = null)
        {
            var order = await _db.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Book)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            if (userId != null && order.UserId != userId)
            {
                throw new Exception("You are not authorized to view this order.");
            }

            return MapToDto(order);
        }

        private OrderResultDto MapToDto(Order o)
        {
            return new OrderResultDto
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                Items = o.Items.Select(i => new OrderItemResultDto
                {
                    BookTitle = i.Book != null ? i.Book.Title : "",
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };
        }
    }
}
