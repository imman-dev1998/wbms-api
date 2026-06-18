
using Microsoft.EntityFrameworkCore;
using wbms_api.Data;
using wbms_api.DTOs;
using wbms_api.Models;

namespace wbms_api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL — List active categories
        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            return await _context.Categories
                .Where(c => c.IsActive)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    RatePerCubicMeter = c.RatePerCubicMeter,
                    MinimumCharge = c.MinimumCharge,
                    IsActive = c.IsActive,
                    CreatedAt = c.CreatedAt,
                    ActiveConsumerCount = c.Consumers.Count(con => con.IsActive)
                })
                .ToListAsync();
        }

        // GET BY ID — View category details
        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    RatePerCubicMeter = c.RatePerCubicMeter,
                    MinimumCharge = c.MinimumCharge,
                    IsActive = c.IsActive,
                    CreatedAt = c.CreatedAt,
                    ActiveConsumerCount = c.Consumers.Count(con => con.IsActive)
                })
                .FirstOrDefaultAsync();
        }

        // CREATE — Add new category
        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                RatePerCubicMeter = dto.RatePerCubicMeter,
                MinimumCharge = dto.MinimumCharge
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                RatePerCubicMeter = category.RatePerCubicMeter,
                MinimumCharge = category.MinimumCharge,
                IsActive = category.IsActive,
                CreatedAt = category.CreatedAt,
                ActiveConsumerCount = 0
            };
        }

        // UPDATE — Modify category details and rates
        public async Task<CategoryDto?> UpdateAsync(int id, UpdateCategoryDto dto)
        {
            var category = await _context.Categories
                .Include(c => c.Consumers)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category is null) return null;

            if (dto.Name is not null) category.Name = dto.Name;
            if (dto.Description is not null) category.Description = dto.Description;
            if (dto.RatePerCubicMeter.HasValue) category.RatePerCubicMeter = dto.RatePerCubicMeter.Value;
            if (dto.MinimumCharge.HasValue) category.MinimumCharge = dto.MinimumCharge.Value;
            if (dto.IsActive.HasValue) category.IsActive = dto.IsActive.Value;

            category.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                RatePerCubicMeter = category.RatePerCubicMeter,
                MinimumCharge = category.MinimumCharge,
                IsActive = category.IsActive,
                CreatedAt = category.CreatedAt,
                ActiveConsumerCount = category.Consumers.Count(c => c.IsActive)
            };
        }

        // DELETE — Safeguarded delete
        public async Task<(bool Success, string Message)> DeleteAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Consumers)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category is null)
                return (false, "Category not found.");

            var activeConsumers = category.Consumers.Count(c => c.IsActive);
            if (activeConsumers > 0)
                return (false, $"Cannot delete. This category has {activeConsumers} active consumer(s). Reassign or deactivate them first.");

            // Soft delete — preserve history
            category.IsActive = false;
            category.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return (true, "Category deleted successfully.");
        }
    }
}
