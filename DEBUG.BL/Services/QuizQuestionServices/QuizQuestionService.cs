using AutoMapper;
using DEBUG.BL.DTOs.QuizQuestionDTOs;
using DEBUG.BL.Exceptions.Common.Common;
using DEBUG.Core.Entities;
using DEBUG.Core.RepositoryInstances;

namespace DEBUG.BL.Services.QuizQuestionServices;

public class QuizQuestionService(IQuizQuestionRepository _repository, IMapper _mapper) : IQuizQuestionService
{
    public async Task<int> CreateAsync(QuizQuestionCreateDTO dto)
    {
        QuizQuestion question = _mapper.Map<QuizQuestion>(dto);
        question.QuizAnswers = _mapper.Map<IEnumerable<QuizAnswer>>(dto.QuizAnswerCreateDTOs);
        await _repository.CreateAsync(question);
        await _repository.SaveChangesAsync();
        return question.Id;
    }

    public async Task<IEnumerable<QuizQuestionGetDTO>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<QuizQuestionGetDTO>>(await _repository.GetWhereAsync(x => x.IsDeleted == false, ["QuizAnswers", "Tag"]));
    }

    public async Task<QuizQuestionGetDTO> GetByIdAsync(int id)
    {
        QuizQuestion? question = await _repository.GetByIdAsync(id, x => x.IsDeleted == false, ["QuizAnswers", "Tag"]);
        if (question == null) throw new NotFoundException<QuizQuestion>();
        return _mapper.Map<QuizQuestionGetDTO>(question);
    }

    public async Task HardDeleteAsync(int id)
    {
        QuizQuestion? target = await _repository.GetByIdAsync(id);
        if (target == null) throw new NotFoundException<QuizQuestion>();
        await _repository.HardDeleteAsync(target);
        await _repository.SaveChangesAsync();
    }

    public async Task SoftDeleteOrRestoreAsync(int id)
    {
        QuizQuestion? target = await _repository.GetByIdAsync(id);
        if (target == null) throw new NotFoundException<QuizQuestion>();
        _repository.SoftDeleteAndRestore(target);
        await _repository.SaveChangesAsync();
    }

    public async Task<QuizQuestionGetDTO> UpdateAsync(int id, QuizQuestionUpdateDTO dto)
    {
        QuizQuestion? question = await _repository.GetByIdAsync(id);
        if (question == null) throw new NotFoundException<Question>();
        _mapper.Map(dto, question);
        await _repository.SaveChangesAsync();
        return _mapper.Map<QuizQuestionGetDTO>(question);
    }
}