using DEBUG.BL.DTOs.AdditionalDTOs;
using DEBUG.BL.DTOs.AnswerDTOs;
using DEBUG.Core.Entities;

namespace DEBUG.BL.Services.AnswerServices;

public interface IAnswerService
{
    Task<int> CreateAsync(int questionId, AnswerCreateDTO dto, User user);
    Task<IEnumerable<AnswerGetDTO>> GetAllByQuestionIdAsync(int questionId, short pageNo, short take);
    Task<AnswerGetDTO> UpdateAsync(int id, AnswerUpdateDTO dto);
    Task HardDeleteAsync(int id);
    Task SoftDeleteOrRestoreAsync(int id);
    Task LikeDislikeAsync(User user, int answerId, bool isLiked);
    Task<LikeDislikeDTO> GetLikeDislikeAsync(int answerId);
    Task<IEnumerable<AnswerGetDTO>> GetByUserIdAsync(string id, short pageNo, short take);
    Task<AnswerGetDTO> GetByIdAsync(int id);
}
