using AutoMapper;
using DEBUG.BL.ViewModels.QuestionVMs;
using DEBUG.Core.Models;
using DEBUG.Core.RepositoryInstances;

namespace DEBUG.BL.Services.QuestionServices;

public class QuestionService(IQuestionRepository _repository, IMapper _mapper) : IQuestionService
{
    public async Task CreateAsync(QuestionCreateVM vm, User user)
    {
        Question question = _mapper.Map<Question>(vm);
        question.UserId = user.Id;
        await _repository.CreateAsync(question);
        await _repository.SaveChangesAsync();
    }

    public IEnumerable<QuestionGetVM> GetAll()
    {
        return _mapper.Map<IEnumerable<QuestionGetVM>>(_repository.GetAll("User"));
    }

    public async Task<QuestionGetVM> GetByIdAsync(int id)
    {
        return _mapper.Map<QuestionGetVM>(await _repository.GetByIdAsync(id, "User"));
    }

    public IEnumerable<QuestionGetVM> GetByUserId(string id)
    {
        return _mapper.Map<IEnumerable<QuestionGetVM>>(_repository.GetWhere(x => x.UserId == id, "User"));
    }

    public async Task HardDeleteAsync(int id)
    {
        Question? target = await _repository.GetByIdAsync(id);
        await _repository.HardDeleteAsync(target);
        await _repository.SaveChangesAsync();
    }

    public async Task SoftDeleteAndRestore(int id)
    {
        Question? target = await _repository.GetByIdAsync(id);
        _repository.SoftDeleteAndRestore(target);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, QuestionCreateVM vm)
    {
        Question? question = await _repository.GetByIdAsync(id);
        _mapper.Map(question, vm);
        await _repository.SaveChangesAsync();
    }
}
