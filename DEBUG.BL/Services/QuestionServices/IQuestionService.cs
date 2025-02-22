using DEBUG.BL.DTOs.AdditionalDTOs;
using DEBUG.BL.DTOs.QuestionDTOs;
using DEBUG.Core.Entities;

namespace DEBUG.BL.Services.QuestionServices;

public interface IQuestionService
{
    Task<int> CreateAsync(int CategoryId, QuestionCreateDTO dto, User user);
    Task<QuestionGetDTO> UpdateAsync(int id, QuestionUpdateDTO dto);
    Task HardDeleteAsync(int id);
    Task SoftDeleteOrRestoreAsync(int id);
    Task LikeDislikeAsync(User user, int questionId, bool isLiked);
    Task<LikeDislikeDTO> GetLikeDislikeAsync(int questionId);
    Task<IEnumerable<QuestionGetDTO>> GetAllAsync();
    Task<IEnumerable<QuestionGetDTO>> GetByCategoryAndTagsAsync(int categoryId, int[] tagIds);
    Task<IEnumerable<QuestionGetDTO>> GetByUserIdAsync(string id);
    Task<QuestionGetDTO> GetByIdAsync(int id);
}
