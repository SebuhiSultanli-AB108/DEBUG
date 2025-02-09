using AutoMapper;
using DEBUG.BL.DTOs.QuestionDTOs;
using DEBUG.BL.Exceptions.Common.Common;
using DEBUG.Core.Entities;
using DEBUG.Core.RepositoryInstances;
using Microsoft.AspNetCore.Identity;

namespace DEBUG.BL.Services.QuestionServices;

public class QuestionService(IQuestionRepository _repository, UserManager<User> _userManager, IMapper _mapper) : IQuestionService
{
    public async Task<int> CreateAsync(QuestionCreateDTO dto, User user)
    {
        Question question = _mapper.Map<Question>(dto);
        question.UserId = user.Id;
        await _repository.CreateAsync(question);
        await _repository.SaveChangesAsync();
        return question.Id;
    }

    public IEnumerable<QuestionGetDTO> GetAll()
    {
        return _mapper.Map<IEnumerable<QuestionGetDTO>>(_repository.GetWhere(x => x.IsDeleted == false, "User"));
    }

    public async Task<QuestionGetDTO> GetByIdAsync(int id)
    {
        Question? question = await _repository.GetByIdAsync(id, x => x.IsDeleted == false, "User");
        if (question == null) throw new NotFoundException<Question>();
        return _mapper.Map<QuestionGetDTO>(question);
    }

    public async Task<IEnumerable<QuestionGetDTO>> GetByUserIdAsync(string id)
    {
        if (await _userManager.FindByIdAsync(id) == null) throw new NotFoundException<User>();
        return _mapper.Map<IEnumerable<QuestionGetDTO>>(_repository.GetWhere(x => x.UserId == id, "User"));
    }
    public async Task HardDeleteAsync(int id)
    {
        Question? target = await _repository.GetByIdAsync(id);
        if (target == null) throw new NotFoundException<Question>();
        await _repository.HardDeleteAsync(target);
        await _repository.SaveChangesAsync();
    }

    public async Task SoftDeleteOrRestoreAsync(int id)
    {
        Question? target = await _repository.GetByIdAsync(id);
        if (target == null) throw new NotFoundException<Question>();
        _repository.SoftDeleteAndRestore(target);
        await _repository.SaveChangesAsync();
    }

    public async Task<QuestionGetDTO> UpdateAsync(int id, QuestionUpdateDTO dto)
    {
        Question? question = await _repository.GetByIdAsync(id);
        if (question == null) throw new NotFoundException<Question>();
        question.UpdatedAt = DateTime.Now;
        _mapper.Map(dto, question);
        await _repository.SaveChangesAsync();
        return _mapper.Map<QuestionGetDTO>(question);
    }
}
