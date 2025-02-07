using AutoMapper;
using DEBUG.BL.ViewModels.AnswerVMs;
using DEBUG.Core.Models;
using DEBUG.Core.RepositoryInstances;

namespace DEBUG.BL.Services.AnswerServices;

public class AnswerService(IAnswerRepository _repository, IMapper _mapper) : IAnswerService
{
    public async Task CreateAsync(AnswerCreateVM vm, int questionId, User user)
    {
        Answer Answer = _mapper.Map<Answer>(vm);
        Answer.UserId = user.Id;
        Answer.QuestionId = questionId;
        await _repository.CreateAsync(Answer);
        await _repository.SaveChangesAsync();
    }

    public IEnumerable<AnswerGetVM> GetAll()
    {
        return _mapper.Map<IEnumerable<AnswerGetVM>>(_repository.GetAll("User"));
    }

    public async Task<AnswerGetVM> GetByIdAsync(int id)
    {
        return _mapper.Map<AnswerGetVM>(await _repository.GetByIdAsync(id, "User"));
    }

    public IEnumerable<AnswerGetVM> GetByQuestionId(int id)
    {
        return _mapper.Map<IEnumerable<AnswerGetVM>>(_repository.GetWhere(x => x.QuestionId == id, "User"));
    }

    public async Task HardDeleteAsync(int id)
    {
        Answer? target = await _repository.GetByIdAsync(id);
        await _repository.HardDeleteAsync(target);
        await _repository.SaveChangesAsync();
    }

    public async Task SoftDeleteAndRestore(int id)
    {
        Answer? target = await _repository.GetByIdAsync(id);
        _repository.SoftDeleteAndRestore(target);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, AnswerCreateVM vm)
    {
        Answer? Answer = await _repository.GetByIdAsync(id);
        _mapper.Map(Answer, vm);
        await _repository.SaveChangesAsync();
    }
}