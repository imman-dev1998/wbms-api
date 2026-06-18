using wbms_api.DTOs;

namespace wbms_api.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CreateCategoryDto dto);
        Task<CategoryDto?> UpdateAsync(int id, UpdateCategoryDto dto);
        Task<(bool Success, string Message)> DeleteAsync(int id);
    }
}
