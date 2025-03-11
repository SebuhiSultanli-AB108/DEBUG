using AutoMapper;
using DEBUG.BL.DTOs.CategoryDTOs;
using DEBUG.BL.Exceptions;
using DEBUG.BL.Exceptions.Common.Common;
using DEBUG.Core.Entities;
using DEBUG.Core.RepositoryInstances;

namespace DEBUG.BL.Services.CategoryServices;

public class CategoryService(ICategoryRepository _repository, IMapper _mapper) : ICategoryService
{
    public async Task<int> CreateAsync(CategoryCreateDTO dto)
    {
        Category category = _mapper.Map<Category>(dto);
        await _repository.CreateAsync(category);
        await _repository.SaveChangesAsync();
        return category.Id;
    }

    public async Task<IEnumerable<CategoryGetDTO>> GetAllAsync(short pageNo, short take)
    {
        if (pageNo <= 0 || take <= 0)
            throw new PageOrTakeCantBeZeroException();
        return _mapper.Map<IEnumerable<CategoryGetDTO>>(await _repository.GetWhereAsync(pageNo, take, x => x.IsDeleted == false));
    }


    public async Task<CategoryGetDTO> GetByIdAsync(int id)
    {
        Category? category = await _repository.GetByIdAsync(id, x => x.IsDeleted == false);
        if (category == null) throw new NotFoundException<Category>();
        return _mapper.Map<CategoryGetDTO>(category);
    }

    public async Task HardDeleteAsync(int id)
    {
        Category? target = await _repository.GetByIdAsync(id);
        if (target == null) throw new NotFoundException<Category>();
        await _repository.HardDeleteAsync(target);
        await _repository.SaveChangesAsync();
    }

    public async Task SoftDeleteOrRestoreAsync(int id)
    {
        Category? target = await _repository.GetByIdAsync(id);
        if (target == null) throw new NotFoundException<Category>();
        _repository.SoftDeleteAndRestore(target);
        await _repository.SaveChangesAsync();
    }

    public async Task<CategoryGetDTO> UpdateAsync(int id, CategoryUpdateDTO dto)
    {
        Category? category = await _repository.GetByIdAsync(id);
        if (category == null) throw new NotFoundException<Category>();
        _mapper.Map(dto, category);
        await _repository.SaveChangesAsync();
        return _mapper.Map<CategoryGetDTO>(category);
    }
}
