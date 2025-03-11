using DEBUG.BL.DTOs.AdditionalDTOs;
using DEBUG.BL.DTOs.CommentDTOs;
using DEBUG.Core.Entities;

namespace DEBUG.BL.Services.CommentServices;

public interface ICommentService
{
    Task<int> CreateAsync(int answerId, CommentCreateDTO dto, User user);
    Task<IEnumerable<CommentGetDTO>> GetAllByAnswerIdAsync(int answerId, short pageNo, short take);
    Task<CommentGetDTO> UpdateAsync(int id, CommentUpdateDTO dto);
    Task HardDeleteAsync(int id);
    Task SoftDeleteOrRestoreAsync(int id);
    Task LikeDislikeAsync(User user, int commentId, bool isLiked);
    Task<LikeDislikeDTO> GetLikeDislikeAsync(int commentId);
    Task<IEnumerable<CommentGetDTO>> GetAllAsync(short pageNo, short take);
    Task<IEnumerable<CommentGetDTO>> GetByUserIdAsync(string id);
    Task<CommentGetDTO> GetByIdAsync(int id);
}
