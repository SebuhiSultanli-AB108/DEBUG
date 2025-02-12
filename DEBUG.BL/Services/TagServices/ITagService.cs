using DEBUG.BL.DTOs.TagDTOs;

namespace DEBUG.BL.Services.TagServices;

public interface ITagService
{
    Task<int> CreateAsync(TagCreateDTO dto);
    Task<TagGetDTO> UpdateAsync(int id, TagUpdateDTO dto);
    Task HardDeleteAsync(int id);
    Task SoftDeleteOrRestoreAsync(int id);
    Task<IEnumerable<TagGetDTO>> GetAllAsync();
    Task<TagGetDTO> GetByIdAsync(int id);
    Task<IEnumerable<TagGetDTO>> GetRangeByIdsAsync(int[] ids);
}
