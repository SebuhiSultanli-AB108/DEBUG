using DEBUG.BL.DTOs.TagDTOs;
using DEBUG.Core.Entities;

namespace DEBUG.BL.Services.TagServices;

public interface ITagService
{
    Task<int> CreateAsync(TagCreateDTO dto);
    Task<TagGetDTO> UpdateAsync(int id, TagUpdateDTO dto);
    Task HardDeleteAsync(int id);
    Task SoftDeleteOrRestoreAsync(int id);
    Task<IEnumerable<TagGetDTO>> GetAllAsync(short pageNo, short take);
    Task<TagGetDTO> GetByIdAsync(int id);
    Task<IEnumerable<Tag>?> GetRangeByIdsAsync(int[] ids);
}
