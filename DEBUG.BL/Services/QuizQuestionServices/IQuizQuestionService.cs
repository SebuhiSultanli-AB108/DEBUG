using DEBUG.BL.DTOs.QuizQuestionDTOs;

namespace DEBUG.BL.Services.QuizQuestionServices;

public interface IQuizQuestionService
{
    Task<int> CreateAsync(QuizQuestionCreateDTO dto);
    Task<QuizQuestionGetDTO> UpdateAsync(int id, QuizQuestionUpdateDTO dto);
    Task HardDeleteAsync(int id);
    Task SoftDeleteOrRestoreAsync(int id);
    Task<IEnumerable<QuizQuestionGetDTO>> GetAllAsync();
    Task<QuizQuestionGetDTO> GetByIdAsync(int id);
}