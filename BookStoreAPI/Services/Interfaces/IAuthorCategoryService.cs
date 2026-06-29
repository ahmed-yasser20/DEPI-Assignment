using BookStoreAPI.Models.DTOs.Authors;
using BookStoreAPI.Models.DTOs.Categories;

namespace BookStoreAPI.Services.Interfaces;

public interface IAuthorService
{
    Task<List<AuthorResponse>> GetAllAsync();
    Task<AuthorResponse?> GetByIdAsync(int id);
    Task<AuthorResponse> CreateAsync(CreateAuthorRequest request);
    Task<AuthorResponse?> UpdateAsync(int id, UpdateAuthorRequest request);
    Task<bool> DeleteAsync(int id);
}

public interface ICategoryService
{
    Task<List<CategoryResponse>> GetAllAsync();
    Task<CategoryResponse?> GetByIdAsync(int id);
    Task<CategoryResponse> CreateAsync(CreateCategoryRequest request);
    Task<CategoryResponse?> UpdateAsync(int id, UpdateCategoryRequest request);
    Task<bool> DeleteAsync(int id);
}
