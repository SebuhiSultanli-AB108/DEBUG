using DEBUG.BL.DTOs.QuizQuestionDTOs;
using DEBUG.Core.Entities;

namespace DEBUG.BL.Services.QuizQuestionServices;

public interface IQuizQuestionService
{
    Task<int> CreateAsync(QuizQuestionCreateDTO dto);
    Task<int?> VerifyQuizAnswersAsync(int questionId, int answerId, User user);
    Task RangedCreateAsync(IEnumerable<QuizQuestionCreateDTO> dtos);
    Task<QuizQuestionGetDTO> UpdateAsync(int id, QuizQuestionUpdateDTO dto);
    Task HardDeleteAsync(int id);
    Task SoftDeleteOrRestoreAsync(int id);
    Task<IEnumerable<QuizQuestionGetDTO>> GetAllAsync();
    Task<IEnumerable<QuizQuestionGetDTO>> Get5RandomQuestionsAsync(int difficulty);
    Task<QuizQuestionGetDTO> GetByIdAsync(int id);
}