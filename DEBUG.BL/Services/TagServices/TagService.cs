using AutoMapper;
using DEBUG.BL.DTOs.TagDTOs;
using DEBUG.BL.Exceptions.Common.Common;
using DEBUG.Core.Entities;
using DEBUG.Core.RepositoryInstances;

namespace DEBUG.BL.Services.TagServices;

public class TagService(ITagRepository _repository, IMapper _mapper) : ITagService
{
    public async Task<int> CreateAsync(TagCreateDTO dto)
    {
        Tag tag = _mapper.Map<Tag>(dto);
        await _repository.CreateAsync(tag);
        await _repository.SaveChangesAsync();
        return tag.Id;
    }

    public async Task<IEnumerable<TagGetDTO>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<TagGetDTO>>(await _repository.GetWhereAsync(x => x.IsDeleted == false));
    }

    public async Task<TagGetDTO> GetByIdAsync(int id)
    {
        Tag? tag = await _repository.GetByIdAsync(id, x => x.IsDeleted == false);
        if (tag == null) throw new NotFoundException<Tag>();
        return _mapper.Map<TagGetDTO>(tag);
    }

    public async Task<IEnumerable<Tag>> GetRangeByIdsAsync(int[] ids)
    {
        return await _repository.GetRangeByIdsAsync(ids);
    }

    public async Task HardDeleteAsync(int id)
    {
        Tag? target = await _repository.GetByIdAsync(id);
        if (target == null) throw new NotFoundException<Tag>();
        await _repository.HardDeleteAsync(target);
        await _repository.SaveChangesAsync();
    }

    public async Task SoftDeleteOrRestoreAsync(int id)
    {
        Tag? target = await _repository.GetByIdAsync(id);
        if (target == null) throw new NotFoundException<Tag>();
        _repository.SoftDeleteAndRestore(target);
        await _repository.SaveChangesAsync();
    }

    public async Task<TagGetDTO> UpdateAsync(int id, TagUpdateDTO dto)
    {
        Tag? tag = await _repository.GetByIdAsync(id);
        if (tag == null) throw new NotFoundException<Tag>();
        _mapper.Map(dto, tag);
        await _repository.SaveChangesAsync();
        return _mapper.Map<TagGetDTO>(tag);
    }
}
