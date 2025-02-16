using AutoMapper;
using DEBUG.BL.DTOs.QuizQuestionDTOs;
using DEBUG.BL.Exceptions.Common.Common;
using DEBUG.Core.Entities;
using DEBUG.Core.Enums;
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
    public async Task<int?> VerifyQuizAnswersAsync(int questionId, int answerId, User user)
    {
        QuizQuestion? question = await _repository.GetByIdAsync(questionId, null, "QuizAnswers");
        if (question == null) throw new NotFoundException<QuizQuestion>();
        QuizAnswer? wrightAnswer = question.QuizAnswers.Where(x => x.IsCorrect == true).FirstOrDefault();
        QuizAnswer? givenAnswer = question.QuizAnswers.Where(x => x.Id == answerId).FirstOrDefault();
        if (givenAnswer == null) throw new NotFoundException<QuizAnswer>();
        if (wrightAnswer.Id == givenAnswer.Id)
        {
            user.CorrectQuizAnswerCount++;
            if (user.CorrectQuizAnswerCount >= 50)
            {
                user.Badges |= Badges.Quiz50;
                user.Badges &= ~Badges.Quiz25;
            }
            else if (user.CorrectQuizAnswerCount >= 25)
                user.Badges |= Badges.Quiz25;
            await _repository.SaveChangesAsync();
            return null;
        }
        else return wrightAnswer.Id;
    }
    public async Task RangedCreateAsync(IEnumerable<QuizQuestionCreateDTO> dtos)
    {
        foreach (var dto in dtos)
        {
            QuizQuestion question = _mapper.Map<QuizQuestion>(dto);
            question.QuizAnswers = _mapper.Map<IEnumerable<QuizAnswer>>(dto.QuizAnswerCreateDTOs);
            await _repository.CreateAsync(question);
        }
        await _repository.SaveChangesAsync();
    }
    public async Task<IEnumerable<QuizQuestionGetDTO>> Get5RandomQuestionsAsync(int difficulty)
    {
        IEnumerable<QuizQuestion> questions = await _repository.GetWhereAsync(x => x.Difficulty == difficulty, ["Tag", "QuizAnswers"]);
        return _mapper.Map<IEnumerable<QuizQuestionGetDTO>>(questions.OrderBy(x => Guid.NewGuid()).Take(5));
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