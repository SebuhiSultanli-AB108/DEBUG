using DEBUG.BL.DTOs.AnswerDTOs;
using DEBUG.Core.Entities;

namespace DEBUG.BL.Services.AnswerServices;

public interface IAnswerService
{
    Task<int> CreateAsync(int questionId, AnswerCreateDTO dto, User user);
    Task<IEnumerable<AnswerGetDTO>> GetAllByQuestionIdAsync(int questionId);
    Task<AnswerGetDTO> UpdateAsync(int id, AnswerUpdateDTO dto);
    Task HardDeleteAsync(int id);
    Task SoftDeleteOrRestoreAsync(int id);
    //IEnumerable<AnswerGetDTO> GetAll();
    Task<IEnumerable<AnswerGetDTO>> GetByUserIdAsync(string id);
    Task<AnswerGetDTO> GetByIdAsync(int id);
}
