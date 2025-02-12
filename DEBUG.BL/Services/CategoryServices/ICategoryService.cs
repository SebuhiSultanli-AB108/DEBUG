using DEBUG.BL.DTOs.CategoryDTOs;

namespace DEBUG.BL.Services.CategoryServices;

public interface ICategoryService
{
    Task<int> CreateAsync(CategoryCreateDTO dto);
    Task<CategoryGetDTO> UpdateAsync(int id, CategoryUpdateDTO dto);
    Task HardDeleteAsync(int id);
    Task SoftDeleteOrRestoreAsync(int id);
    Task<IEnumerable<CategoryGetDTO>> GetAllAsync();
    Task<CategoryGetDTO> GetByIdAsync(int id);
}
