using BookStoreAPI.Models.DTOs;
using BookStoreAPI.Models.DTOs.Books;

namespace BookStoreAPI.Services.Interfaces;

public interface IBookService
{
    Task<PagedResult<BookResponse>> GetBooksAsync(BookQueryParams queryParams);
    Task<BookResponse?> GetBookByIdAsync(int id);
    Task<BookResponse> CreateBookAsync(CreateBookRequest request);
    Task<BookResponse?> UpdateBookAsync(int id, UpdateBookRequest request);
    Task<bool> DeleteBookAsync(int id);
}
