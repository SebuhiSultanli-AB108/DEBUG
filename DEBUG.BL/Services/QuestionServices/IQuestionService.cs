using DEBUG.BL.DTOs.QuestionDTOs;
using DEBUG.Core.Entities;

namespace DEBUG.BL.Services.QuestionServices;

public interface IQuestionService
{
    Task<int> CreateAsync(QuestionCreateDTO dto, User user);
    Task<QuestionGetDTO> UpdateAsync(int id, QuestionUpdateDTO dto);
    Task HardDeleteAsync(int id);
    Task SoftDeleteOrRestoreAsync(int id);
    IEnumerable<QuestionGetDTO> GetAll();
    Task<IEnumerable<QuestionGetDTO>> GetByUserIdAsync(string id);
    Task<QuestionGetDTO> GetByIdAsync(int id);
}
